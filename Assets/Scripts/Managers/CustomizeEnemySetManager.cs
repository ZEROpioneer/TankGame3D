using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomizeEnemySetManager : MonoBehaviour
{
    [Header("生成设置")]
    public TMP_InputField maxEnemyCountInputField;    // 在场最多敌人输入框
    public TMP_InputField spawnIntervalInputField;    // 生成时间间隔输入框
    public TMP_InputField enemyIncrementInputField;   // 递增敌人数输入框

    [Header("生命值设置")]
    public TMP_InputField maxHealthInputField;        // 最大生命值输入框
    
    [Header("伤害设置")]
    public TMP_InputField DamageInputField;           // 子弹伤害输入框

    [Header("UI控制")]
    public Button backToPrevButton;       // 返回上一级按钮
    public Button resetDefaultButton;     // 恢复默认值按钮
    public GameObject CustomizeEnemySetPanel;   // 生成设置面板自身
    public GameObject EnemySetPanel;       // 上一级设置面板（比如敌人设置主面板等）

    [Header("默认参数值")]
    [SerializeField] private int defaultMaxEnemyCount = 5;
    [SerializeField] private float defaultSpawnInterval = 2f;
    [SerializeField] private int defaultEnemyIncrement = 2;
    [SerializeField] private int defaultMaxHealth = 3;
    [SerializeField] private int defaultDamage = 1;

    // 当前参数值（供其他脚本访问，根据实际需求决定是否需要公开）
    public static int CurrentMaxEnemyCount { get; private set; }
    public static float CurrentSpawnInterval { get; private set; }
    public static int CurrentEnemyIncrement { get; private set; }
    public static int CurrentMaxHealth { get; private set; }
    public static int CurrentDamage { get; private set; }

    void Start()
    {
        // 初始化界面
        InitializeSettings();
        // 绑定事件
        BindInputFieldEvents();
        BindButtonEvents();
    }

    /// <summary>
    /// 初始化设置界面，加载设置或使用默认值并更新显示
    /// </summary>
    void InitializeSettings()
    {
        LoadSettings();
        UpdateInputFieldValues();
    }

    /// <summary>
    /// 加载设置，从PlayerPrefs读取或使用默认值
    /// </summary>
    void LoadSettings()
    {
        CurrentMaxEnemyCount = PlayerPrefs.GetInt("MaxEnemyCount", defaultMaxEnemyCount);
        CurrentSpawnInterval = PlayerPrefs.GetFloat("SpawnInterval", defaultSpawnInterval);
        CurrentEnemyIncrement = PlayerPrefs.GetInt("EnemyIncrement", defaultEnemyIncrement);
        CurrentMaxHealth = PlayerPrefs.GetInt("EnemyMaxHealth", defaultMaxHealth);
        CurrentDamage = PlayerPrefs.GetInt("EnemyDamage", defaultDamage);
    }

    /// <summary>
    /// 保存设置到PlayerPrefs
    /// </summary>
    void SaveSettings()
    {
        PlayerPrefs.SetInt("MaxEnemyCount", CurrentMaxEnemyCount);
        PlayerPrefs.SetFloat("SpawnInterval", CurrentSpawnInterval);
        PlayerPrefs.SetInt("EnemyIncrement", CurrentEnemyIncrement);
        PlayerPrefs.SetInt("EnemyMaxHealth", CurrentMaxHealth);
        PlayerPrefs.SetInt("EnemyDamage", CurrentDamage);
        PlayerPrefs.Save();
        Debug.Log("生成设置已保存");
    }

    /// <summary>
    /// 更新输入框显示的数值
    /// </summary>
    void UpdateInputFieldValues()
    {
        if (maxEnemyCountInputField != null)
            maxEnemyCountInputField.text = CurrentMaxEnemyCount.ToString();
        if (spawnIntervalInputField != null)
            spawnIntervalInputField.text = CurrentSpawnInterval.ToString("F1");
        if (enemyIncrementInputField != null)
            enemyIncrementInputField.text = CurrentEnemyIncrement.ToString();
        if (maxHealthInputField != null)
            maxHealthInputField.text = CurrentMaxHealth.ToString();
        if (DamageInputField != null)
            DamageInputField.text = CurrentDamage.ToString();
    }

    /// <summary>
    /// 绑定输入框的结束编辑事件
    /// </summary>
    void BindInputFieldEvents()
    {
        if (maxEnemyCountInputField != null)
            maxEnemyCountInputField.onEndEdit.AddListener(OnMaxEnemyCountChanged);
        if (spawnIntervalInputField != null)
            spawnIntervalInputField.onEndEdit.AddListener(OnSpawnIntervalChanged);
        if (enemyIncrementInputField != null)
            enemyIncrementInputField.onEndEdit.AddListener(OnEnemyIncrementChanged);
        if (maxHealthInputField != null)
            maxHealthInputField.onEndEdit.AddListener(OnMaxHealthChanged);
        if (DamageInputField != null)
            DamageInputField.onEndEdit.AddListener(OnDamageChanged);
    }

    /// <summary>
    /// 绑定按钮的点击事件
    /// </summary>
    void BindButtonEvents()
    {
        if (backToPrevButton != null)
            backToPrevButton.onClick.AddListener(OnBackToPrevClick);
        if (resetDefaultButton != null)
            resetDefaultButton.onClick.AddListener(ResetToDefaults);
    }

    #region 输入框值改变事件处理
    void OnMaxEnemyCountChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentMaxEnemyCount = Mathf.Max(1, newValue); // 限制最小值为1，可根据需求调整
            maxEnemyCountInputField.text = CurrentMaxEnemyCount.ToString();
        }
        else
        {
            // 输入无效，恢复原来显示的值
            maxEnemyCountInputField.text = CurrentMaxEnemyCount.ToString();
        }
    }

    void OnSpawnIntervalChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentSpawnInterval = Mathf.Max(0.1f, newValue); // 限制最小值为0.1，可调整
            spawnIntervalInputField.text = CurrentSpawnInterval.ToString("F1");
        }
        else
        {
            spawnIntervalInputField.text = CurrentSpawnInterval.ToString("F1");
        }
    }

    void OnEnemyIncrementChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentEnemyIncrement = Mathf.Max(1, newValue); // 限制最小值为1
            enemyIncrementInputField.text = CurrentEnemyIncrement.ToString();
        }
        else
        {
            enemyIncrementInputField.text = CurrentEnemyIncrement.ToString();
        }
    }

    void OnMaxHealthChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentMaxHealth = Mathf.Max(1, newValue); // 限制最小值为1
            maxHealthInputField.text = CurrentMaxHealth.ToString();
        }
        else
        {
            maxHealthInputField.text = CurrentMaxHealth.ToString();
        }
    }
    
    void OnDamageChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentDamage = Mathf.Max(1, newValue); // 限制最小值为1
            DamageInputField.text = CurrentDamage.ToString();
        }
        else
        {
            DamageInputField.text = CurrentDamage.ToString();
        }
    }
    #endregion

    /// <summary>
    /// 返回上一级按钮点击事件
    /// </summary>
    void OnBackToPrevClick()
    {
        SaveSettings();
        // 隐藏当前生成设置面板
        if (CustomizeEnemySetPanel != null)
            CustomizeEnemySetPanel.SetActive(false);
        // 显示上一级设置面板
        if (EnemySetPanel != null)
            EnemySetPanel.SetActive(true);
        Debug.Log("返回上一级设置面板");
    }

    /// <summary>
    /// 重置为默认值
    /// </summary>
    public void ResetToDefaults()
    {
        CurrentMaxEnemyCount = defaultMaxEnemyCount;
        CurrentSpawnInterval = defaultSpawnInterval;
        CurrentEnemyIncrement = defaultEnemyIncrement;
        CurrentMaxHealth = defaultMaxHealth;
        CurrentDamage = defaultDamage;

        UpdateInputFieldValues();
        SaveSettings();
        Debug.Log("生成设置已重置为默认值");
    }

    void OnDestroy()
    {
        // 移除输入框事件监听，避免潜在的内存泄漏和异常
        if (maxEnemyCountInputField != null)
            maxEnemyCountInputField.onEndEdit.RemoveAllListeners();
        if (spawnIntervalInputField != null)
            spawnIntervalInputField.onEndEdit.RemoveAllListeners();
        if (enemyIncrementInputField != null)
            enemyIncrementInputField.onEndEdit.RemoveAllListeners();
        if (maxHealthInputField != null)
            maxHealthInputField.onEndEdit.RemoveAllListeners();
        if (DamageInputField != null)
            DamageInputField.onEndEdit.RemoveAllListeners();
        if (backToPrevButton != null)
            backToPrevButton.onClick.RemoveAllListeners();
        if (resetDefaultButton != null)
            resetDefaultButton.onClick.RemoveAllListeners();
    }
}