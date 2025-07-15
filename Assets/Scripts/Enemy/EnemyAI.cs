using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region 参数设置
    //公共参数
    //这些变量分别控制敌人的行为逻辑和射击行为的参数。大多是通过 Inspector 面板调节的，支持实时调参。
    //私有参数
    //这些变量用于控制敌人的导航与状态切换，包括用于巡逻随机点、玩家距离判断等。
    #endregion
    [Header("AI设置")]
    public float detectionRange = 10f;  // 侦测范围
    public float attackRange = 8f;  // 攻击范围
    public float patrolRadius = 5f;  // 巡逻半径
    public float fireRate = 1f;  // 开火间隔
    
    [Header("移动设置")]
    public float rotationSpeed = 90f;  // 面向玩家旋转速度
    
    [Header("武器设置")]
    public GameObject bulletPrefab;  // 子弹预制体
    public Transform firePoint;  // 子弹发射点
    
    private NavMeshAgent agent;  // 用于导航移动
    private Transform player;  // 玩家对象引用
    private float nextFireTime = 0f;  
    private Vector3 startPosition;  // 初始生成位置（作为巡逻中心）
    private Vector3 patrolTarget;  // 当前巡逻目标点
    private float patrolTimer = 0f;
    private float patrolChangeInterval = 3f;
    
    public enum AIState  //三种状态
    {
        Patrol,  //巡逻
        Chase,  //追逐
        Attack  //攻击
    }
    
    public AIState currentState = AIState.Patrol;
    
    void Start()
    {
        //获取导航组件；
        //找到玩家对象并缓存 Transform；
        //记录起始位置作为巡逻中心；
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPosition = transform.position;
        
        // 设置NavMeshAgent参数
        agent.speed = 3.5f;
        agent.stoppingDistance = 0.5f;  //stoppingDistance 防止移动时撞上目标
        agent.angularSpeed = rotationSpeed;  //angularSpeed 控制转向速度。
        
        SetRandomPatrolTarget();  //设定一个随机巡逻目标点。
    }
    
    void Update()
    {
        if (player == null) return;
        //计算敌人与玩家之间的实时距离。
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        

        #region 状态机
        //这是一套基于条件判断的有限状态机（FSM）实现方式：
        //如果敌人在巡逻状态中且玩家靠近，切换为追击；
        //追击中接近到攻击范围，切换为攻击；
        //距离太远会退回到巡逻状态。
        #endregion
        switch (currentState)
        {
            case AIState.Patrol:
                PatrolBehavior();
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = AIState.Chase;
                }
                break;
                
            case AIState.Chase:
                ChaseBehavior();
                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attack;
                }
                else if (distanceToPlayer > detectionRange * 1.5f)
                {
                    currentState = AIState.Patrol;
                }
                break;
                
            case AIState.Attack:
                AttackBehavior();
                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Chase;
                }
                break;
        }
    }
    
    void PatrolBehavior()
    {
        // 巡逻行为
        if (agent.remainingDistance < 0.5f)
        {
            //当快到达目标点，就设置一个新的巡逻点
            SetRandomPatrolTarget();
        }
        
        //也可以定时换点（防止卡住）。
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolChangeInterval)
        {
            SetRandomPatrolTarget();
            patrolTimer = 0f;
        }
    }
    
    void ChaseBehavior()
    {
        // 追击行为
        agent.SetDestination(player.position); //设置目标为玩家
        LookAtPlayer();  //同时让敌人朝向玩家
    }
    
    void AttackBehavior()
    {
        // 攻击行为
        agent.SetDestination(transform.position); // 停止移动
        LookAtPlayer();  //面朝玩家；
        
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;  //每隔 fireRate 秒发射一颗子弹。
        }
    }
    
    void SetRandomPatrolTarget()
    {
        // 在起始位置周围随机选择巡逻点
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += startPosition;
        
        //在 NavMesh 上找到最接近的合法位置；
        //设置为新的目标点。
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            patrolTarget = hit.position;
            agent.SetDestination(patrolTarget);
        }
    }
    
    void LookAtPlayer()
    {
        // 让坦克面向玩家
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 保持水平
        
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed / 90f);
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // 创建敌人子弹
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // 设置子弹为敌人发射的
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet == null)
            {
                bullet.AddComponent<EnemyBullet>();
            }
            
            // 给子弹添加速度
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * 20f;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        //用颜色在 Scene 视图中显示敌人的感知范围、攻击范围、巡逻范围；
        // 在Scene视图中显示检测范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, patrolRadius);
    }
}
