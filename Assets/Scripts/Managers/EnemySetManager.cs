using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 存储单种难度的敌人参数
[System.Serializable]
public struct DifficultySettings
{
    public int maxEnemyCount;      // 最大敌人数量
    public float spawnInterval;    // 生成间隔
    public int enemyIncrement;     // 敌人增量
    public int maxHealth;          // 最大生命值
    public int damage;             // 伤害值
}

public class EnemySetManager : MonoBehaviour
{
    [Header("难度选择组件")]
    public TMP_Dropdown difficultyDropdown;  // 难度下拉框

    [Header("难度参数配置（固定值）")]
    [Tooltip("简单难度参数")]
    public DifficultySettings easySettings = new DifficultySettings
    {
        // 场景 / 预制体的序列化数据优先级高于代码默认值
        // 所以会导致 Inspector 面板修改的参数 优先于代码中的
        maxEnemyCount = 3,
        spawnInterval = 2f,
        enemyIncrement = 2,
        maxHealth = 3,
        damage = 1,
    };

    [Tooltip("中等难度参数")]
    public DifficultySettings normalSettings = new DifficultySettings
    {
        maxEnemyCount = 5,
        spawnInterval = 1f,
        enemyIncrement = 3,
        maxHealth = 5,
        damage = 2,
    };

    [Tooltip("困难难度参数")]
    public DifficultySettings hardSettings = new DifficultySettings
    {
        maxEnemyCount = 8,
        spawnInterval = 0.5f,
        enemyIncrement = 5,
        maxHealth = 8,
        damage = 3,
    };

    [Header("当前面板组件")]
    public GameObject currentPanel;  // 当前面板根对象

    [Header("UI控制")]
    public GameObject customizeEnemySetPanel;  // 自定义设置面板
    public GameObject setPanel;                // 上一级设置面板
    public Button customizeButton;             // 自定义设置按钮
    public Button backButton;                  // 返回按钮

    // 静态属性：供其他脚本访问当前选中的难度参数
    public static DifficultySettings CurrentDifficulty { get; private set; }

    private void Start()
    {
        // 初始化下拉框
        InitDropdown();

        // 绑定按钮事件
        customizeButton.onClick.AddListener(OnCustomizeButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);

        // 默认选中简单难度
        difficultyDropdown.value = 0;
        CurrentDifficulty = easySettings;
    }

    /// <summary>
    /// 初始化下拉框选项
    /// </summary>
    private void InitDropdown()
    {
        // 清空默认选项
        difficultyDropdown.ClearOptions();

        // 添加难度选项
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("简单"),
            new TMP_Dropdown.OptionData("中等"),
            new TMP_Dropdown.OptionData("困难")
        };
        difficultyDropdown.options = options;

        // 绑定下拉框选择事件
        difficultyDropdown.onValueChanged.AddListener(OnDifficultySelected);
    }

    /// <summary>
    /// 难度选择改变时的回调
    /// </summary>
    /// <param name="index">选中的索引（0=简单，1=中等，2=困难）</param>
    private void OnDifficultySelected(int index)
    {
        // 根据选择的索引更新当前难度参数
        switch (index)
        {
            case 0:
                CurrentDifficulty = easySettings;
                break;
            case 1:
                CurrentDifficulty = normalSettings;
                break;
            case 2:
                CurrentDifficulty = hardSettings;
                break;
        }

        // 调试用：打印当前难度参数
        Debug.Log($"选择难度：{difficultyDropdown.options[index].text}");
        Debug.Log($"最大敌人数量：{CurrentDifficulty.maxEnemyCount}，生成间隔：{CurrentDifficulty.spawnInterval}");
        Debug.Log($"敌人增量：{CurrentDifficulty.enemyIncrement}，最大生命值：{CurrentDifficulty.maxHealth}，伤害：{CurrentDifficulty.damage}");
    }

    // “自定义设置”按钮点击事件
    private void OnCustomizeButtonClick()
    {
        currentPanel.SetActive(false);
        customizeEnemySetPanel.SetActive(true);
    }

    // “返回上一级”按钮点击事件
    private void OnBackButtonClick()
    {
        currentPanel.SetActive(false);
        setPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        // 移除事件监听，避免内存泄漏
        difficultyDropdown.onValueChanged.RemoveAllListeners();
        customizeButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
    }
}
    