using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("敌人子弹设置")]
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;
    
    void Start()
    {
        // 设置子弹生命周期
        Destroy(gameObject, lifeTime);  //lifeTime后 销毁对象
        
        // 设置子弹材质颜色为红色
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 击中墙壁或障碍物
        if (other.CompareTag("Wall") )
        {
            // 判断是否是砖墙（通过父对象名）
            Transform parent = other.transform.parent;
            if (parent != null && parent.name.Contains("BrickWalls"))
            {
                Destroy(other.gameObject); // 摧毁砖墙
            }
            
            Destroy(gameObject);
        }
        
        // 击中玩家
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
        
        // // 击中基地
        // if (other.CompareTag("Base"))
        // // 游戏结束逻辑
        //     Debug.Log("基地被摧毁！游戏结束！");
        //     Destroy(other.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
