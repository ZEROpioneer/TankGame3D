using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人参数控制器：根据Toggle状态决定使用难度预设或自定义参数
/// 其他脚本通过此类统一获取敌人参数，无需关心具体来源
/// </summary>
[RequireComponent(typeof(ToggleGroup))]
public class EnemyParameterManager : MonoBehaviour
{
    [Header("模式选择Toggle")]
    [Tooltip("勾选时使用难度预设参数（与EnemySetManager关联）")]
    public Toggle difficultyModeToggle;
    
    [Tooltip("勾选时使用自定义参数（与CustomizeEnemySetManager关联）")]
    public Toggle customModeToggle;

    [Header("参数来源脚本")]
    [Tooltip("难度预设参数的管理脚本（存放简单/中等/困难参数）")]
    public EnemySetManager difficultySettings;
    
    [Tooltip("自定义参数的管理脚本（存放玩家手动设置的参数）")]
    public CustomizeEnemySetManager customSettings;

    /// <summary>
    /// 单例实例：其他脚本通过此属性访问参数
    /// </summary>
    public static EnemyParameterManager Instance { get; private set; }

    private void Awake()
    {
        // 单例初始化（确保场景中只有一个参数控制器）
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("场景中存在多个EnemyParameterController，已自动销毁重复实例");
        }
    }

    private void Start()
    {
        // 初始化检查：避免配置错误导致参数获取失败
        ValidateSetup();

        // 确保初始状态有且仅有一个Toggle被选中
        InitializeToggleState();
    }

    /// <summary>
    /// 验证配置是否正确（Toggle分组、脚本引用是否完整）
    /// </summary>
    private void ValidateSetup()
    {
        // 检查Toggle是否属于同一组
        if (difficultyModeToggle.group != customModeToggle.group)
        {
            Debug.LogError("错误：两个Toggle必须属于同一个ToggleGroup！请在Inspector中配置相同的Group");
        }

        // 检查脚本引用是否为空
        if (difficultySettings == null)
        {
            Debug.LogError("错误：未赋值difficultySettings（请关联EnemySetManager脚本）");
        }
        if (customSettings == null)
        {
            Debug.LogError("错误：未赋值customSettings（请关联CustomizeEnemySetManager脚本）");
        }
    }

    /// <summary>
    /// 初始化Toggle状态：确保默认有一个模式被选中
    /// </summary>
    private void InitializeToggleState()
    {
        if (!difficultyModeToggle.isOn && !customModeToggle.isOn)
        {
            // 若初始均未选中，默认激活难度模式
            difficultyModeToggle.isOn = true;
            Debug.Log("初始未选择模式，自动激活【难度选择】模式");
        }
    }

    #region 对外提供的参数访问接口
    /// <summary>
    /// 最大敌人数量（当前生效值）
    /// </summary>
    public int MaxEnemyCount
    {
        get
        {
            return difficultyModeToggle.isOn 
                ? EnemySetManager.CurrentDifficulty.maxEnemyCount  // 难度模式：从预设获取
                : CustomizeEnemySetManager.CurrentMaxEnemyCount;      // 自定义模式：从自定义获取
        }
    }

    /// <summary>
    /// 敌人生成间隔（秒，当前生效值）
    /// </summary>
    // 定义一个公共的“生成间隔”属性（供其他脚本读取）
    public float SpawnInterval
    {
        // 只提供 get（读取）功能，不允许外部修改
        get
        {
            // 三元运算符：条件 ? 条件为真时返回的值 : 条件为假时返回的值
            return difficultyModeToggle.isOn  // 条件：判断“难度模式Toggle”是否被选中
                ? EnemySetManager.CurrentDifficulty.spawnInterval  // 真：返回难度预设的生成间隔
                : CustomizeEnemySetManager.CurrentSpawnInterval;      // 假：返回自定义的生成间隔
        }
    }

    /// <summary>
    /// 敌人增量（每波增加数量，当前生效值）
    /// </summary>
    public int EnemyIncrement
    {
        get
        {
            return difficultyModeToggle.isOn 
                ? EnemySetManager.CurrentDifficulty.enemyIncrement 
                : CustomizeEnemySetManager.CurrentEnemyIncrement;
        }
    }

    /// <summary>
    /// 敌人最大生命值（当前生效值）
    /// </summary>
    public int MaxHealth
    {
        get
        {
            return difficultyModeToggle.isOn 
                ? EnemySetManager.CurrentDifficulty.maxHealth 
                : CustomizeEnemySetManager.CurrentMaxHealth;
        }
    }

    /// <summary>
    /// 敌人伤害值（当前生效值）
    /// </summary>
    public int Damage
    {
        get
        {
            return difficultyModeToggle.isOn 
                ? EnemySetManager.CurrentDifficulty.damage 
                : CustomizeEnemySetManager.CurrentDamage;
        }
    }
    #endregion
}