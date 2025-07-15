using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("生命值设置")]
    public int maxHealth = 3;
    public int currentHealth;
    
    [Header("效果设置")]
    public GameObject explosionEffect; // 爆炸特效预制体
    
    void Start()
    {
        currentHealth = maxHealth;  // 初始化血量
    }

    #region 接受伤害函数
    //函数对外开放（public），其他对象（比如子弹）可以通过
    //GetComponent<HealthSystem>().TakeDamage() 调用；
    
    #endregion
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 扣除血量
        
        if (currentHealth <= 0)
        {
            Die();  // 血量为零死亡
        }
    }

    #region 死亡逻辑处理
    //

    #endregion
    void Die()
    {
        // 播放爆炸效果
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        
        // 如果是玩家，重新生成或游戏结束
        if (CompareTag("Player"))
        {
            // 这里可以调用游戏管理器的玩家死亡处理
            Debug.Log("玩家死亡！");
            
            // 可以在这里调用 GameManager 的方法：GameManager.Instance.OnPlayerDead();
        }
        
        Destroy(gameObject);
    }

    #region 治疗函数

    //治疗时将生命值加上指定数值；
    //Mathf.Min() 保证血量不会超过最大生命值；
    //这是为“回血道具”、“回血技能”留的接口。

    #endregion
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
}
