using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("子弹设置")]
    public float speed = 20f;  // 子弹速度（米/秒）
    public float lifeTime = 5f;  // 子弹最大生存时间（秒）
    public int damage = 1;  // 子弹造成的伤害值
    
    void Start()
    {
        // 设置子弹生命周期
        Destroy(gameObject, lifeTime);  // 在 lifeTime 秒后自动销毁子弹
        
        // 设置子弹速度
        Rigidbody rb = GetComponent<Rigidbody>();  
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;   // 设置初速度，向当前 forward 方向飞行
        }
    }
    
    // 子弹碰撞处理
    void OnTriggerEnter(Collider other)
    {
        // 击中墙壁或障碍物
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))  //根据layer层判断
        {
            // 判断是否是砖墙（通过父对象名）
            Transform parent = other.transform.parent;
            if (parent != null && parent.name.Contains("BrickWalls"))
            {
                Destroy(other.gameObject); // 摧毁砖墙
            }
            
            Destroy(gameObject); // 摧毁子弹
        }
        ///////////////////////////////////////////////////击中敌人待定
        // 击中敌人
        if (other.CompareTag("Enemy"))
        {
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        
            // 通知生成器敌人被摧毁
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.OnEnemyDestroyed();
            }
        
            Destroy(gameObject);
        }
    }
}
