using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("敌人子弹设置")]
    public float speed = 20f;
    public float lifeTime = 5f;
    private int damage; // 改为私有字段，不在初始化时赋值

    void Start()
    {
        // 延迟到Start()中获取伤害值（此时单例已初始化）
        InitializeDamage();

        // 设置子弹生命周期
        Destroy(gameObject, lifeTime);
        
        // 设置子弹材质颜色为红色
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }

    /// <summary>
    /// 初始化伤害值，包含空引用检查
    /// </summary>
    private void InitializeDamage()
    {
        // 检查单例是否存在
        if (EnemyParameterManager.Instance == null)
        {
            Debug.LogError("EnemyParameterManager 实例不存在！请确保场景中有该脚本的实例");
            damage = 1; // 提供默认值，避免后续报错
            return;
        }

        // 安全获取伤害值
        damage = EnemyParameterManager.Instance.Damage;
    }

    void OnTriggerEnter(Collider other)
    {
        // 击中墙壁或障碍物
        if (other.CompareTag("Wall"))
        {
            Transform parent = other.transform.parent;
            if (parent != null && parent.name.Contains("BrickWalls"))
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }

        // 击中玩家
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                // 使用提前初始化的damage变量，避免重复访问单例
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}