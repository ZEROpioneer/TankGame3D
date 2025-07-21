using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySetManager : MonoBehaviour
{
    [Header("波次设置")]
    public TMP_InputField spawnIntervalInput;        // 生成时间间隔
    public TMP_InputField enemiesPerWaveInput;      // 每波敌人数
    public TMP_InputField attackWavesInput;         // 波次攻击次数
    
    [Header("敌人设置")]
    public TMP_InputField avoidanceRadiusInput;     // 递增敌人数
    public TMP_InputField enemyHealthInput;         // 敌人生命值
    public TMP_InputField enemyMaxHealthInput;      // 敌人最大生命值
    
    [Header("UI控制")]
    public Button backButton;          // 返回上一页按钮
    public Button resetButton;         // 重置默认值按钮
    public GameObject waveSetPanel;    // 波次设置面板
    public GameObject mainSetPanel;    // 主设置菜单面板
    
    [Header("默认参数值")]
    [SerializeField] private float defaultSpawnInterval = 5f;
    [SerializeField] private int defaultEnemiesPerWave = 10;
    [SerializeField] private int defaultAttackWaves = 5;
    [SerializeField] private float defaultAvoidanceRadius = 1.5f;
    [SerializeField] private float defaultEnemyHealth = 50f;
    [SerializeField] private float defaultEnemyMaxHealth = 100f;
    
    // 当前参数值（供其他脚本访问）
    public static float CurrentSpawnInterval { get; private set; }
    public static int CurrentEnemiesPerWave { get; private set; }
    public static int CurrentAttackWaves { get; private set; }
    public static float CurrentAvoidanceRadius { get; private set; }
    public static float CurrentEnemyHealth { get; private set; }
    public static float CurrentEnemyMaxHealth { get; private set; }
    
    void Start()
    {
        // 初始化界面
        InitializeSettings();
        
        // 绑定事件
        BindInputFieldEvents();
        BindButtonEvents();
    }
    
    /// <summary>
    /// 初始化设置界面
    /// </summary>
    void InitializeSettings()
    {
        // 加载保存的设置或使用默认值
        LoadSettings();
        
        // 更新输入框显示
        UpdateInputFieldValues();
    }
    
    /// <summary>
    /// 加载设置（从PlayerPrefs或使用默认值）
    /// </summary>
    void LoadSettings()
    {
        CurrentSpawnInterval = PlayerPrefs.GetFloat("SpawnInterval", defaultSpawnInterval);
        CurrentEnemiesPerWave = PlayerPrefs.GetInt("EnemiesPerWave", defaultEnemiesPerWave);
        CurrentAttackWaves = PlayerPrefs.GetInt("AttackWaves", defaultAttackWaves);
        CurrentAvoidanceRadius = PlayerPrefs.GetFloat("AvoidanceRadius", defaultAvoidanceRadius);
        CurrentEnemyHealth = PlayerPrefs.GetFloat("EnemyHealth", defaultEnemyHealth);
        CurrentEnemyMaxHealth = PlayerPrefs.GetFloat("EnemyMaxHealth", defaultEnemyMaxHealth);
    }
    
    /// <summary>
    /// 保存设置到PlayerPrefs
    /// </summary>
    void SaveSettings()
    {
        PlayerPrefs.SetFloat("SpawnInterval", CurrentSpawnInterval);
        PlayerPrefs.SetInt("EnemiesPerWave", CurrentEnemiesPerWave);
        PlayerPrefs.SetInt("AttackWaves", CurrentAttackWaves);
        PlayerPrefs.SetFloat("AvoidanceRadius", CurrentAvoidanceRadius);
        PlayerPrefs.SetFloat("EnemyHealth", CurrentEnemyHealth);
        PlayerPrefs.SetFloat("EnemyMaxHealth", CurrentEnemyMaxHealth);
        PlayerPrefs.Save();
        
        Debug.Log("波次设置已保存");
    }
    
    /// <summary>
    /// 更新输入框数值显示
    /// </summary>
    void UpdateInputFieldValues()
    {
        if (spawnIntervalInput != null)
            spawnIntervalInput.text = CurrentSpawnInterval.ToString("F1");
            
        if (enemiesPerWaveInput != null)
            enemiesPerWaveInput.text = CurrentEnemiesPerWave.ToString();
            
        if (attackWavesInput != null)
            attackWavesInput.text = CurrentAttackWaves.ToString();
            
        if (avoidanceRadiusInput != null)
            avoidanceRadiusInput.text = CurrentAvoidanceRadius.ToString("F2");
            
        if (enemyHealthInput != null)
            enemyHealthInput.text = CurrentEnemyHealth.ToString("F0");
            
        if (enemyMaxHealthInput != null)
            enemyMaxHealthInput.text = CurrentEnemyMaxHealth.ToString("F0");
    }
    
    /// <summary>
    /// 绑定输入框事件
    /// </summary>
    void BindInputFieldEvents()
    {
        if (spawnIntervalInput != null)
            spawnIntervalInput.onEndEdit.AddListener(OnSpawnIntervalChanged);
            
        if (enemiesPerWaveInput != null)
            enemiesPerWaveInput.onEndEdit.AddListener(OnEnemiesPerWaveChanged);
            
        if (attackWavesInput != null)
            attackWavesInput.onEndEdit.AddListener(OnAttackWavesChanged);
            
        if (avoidanceRadiusInput != null)
            avoidanceRadiusInput.onEndEdit.AddListener(OnAvoidanceRadiusChanged);
            
        if (enemyHealthInput != null)
            enemyHealthInput.onEndEdit.AddListener(OnEnemyHealthChanged);
            
        if (enemyMaxHealthInput != null)
            enemyMaxHealthInput.onEndEdit.AddListener(OnEnemyMaxHealthChanged);
    }
    
    /// <summary>
    /// 绑定按钮事件
    /// </summary>
    void BindButtonEvents()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnBackClick);
        
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetToDefaults);
    }
  
    #region 输入框值改变事件
    void OnSpawnIntervalChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentSpawnInterval = Mathf.Max(0.5f, newValue); // 最小值限制0.5秒
            spawnIntervalInput.text = CurrentSpawnInterval.ToString("F1");
        }
        else
        {
            spawnIntervalInput.text = CurrentSpawnInterval.ToString("F1");
        }
    }
    
    void OnEnemiesPerWaveChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentEnemiesPerWave = Mathf.Max(1, newValue); // 至少1个敌人
            enemiesPerWaveInput.text = CurrentEnemiesPerWave.ToString();
        }
        else
        {
            enemiesPerWaveInput.text = CurrentEnemiesPerWave.ToString();
        }
    }
    
    void OnAttackWavesChanged(string value)
    {
        if (int.TryParse(value, out int newValue))
        {
            CurrentAttackWaves = Mathf.Max(1, newValue); // 至少1波
            attackWavesInput.text = CurrentAttackWaves.ToString();
        }
        else
        {
            attackWavesInput.text = CurrentAttackWaves.ToString();
        }
    }
    
    void OnAvoidanceRadiusChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentAvoidanceRadius = Mathf.Max(0.1f, newValue); // 最小半径0.1
            avoidanceRadiusInput.text = CurrentAvoidanceRadius.ToString("F2");
        }
        else
        {
            avoidanceRadiusInput.text = CurrentAvoidanceRadius.ToString("F2");
        }
    }
    
    void OnEnemyHealthChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            // 确保生命值不超过最大生命值
            CurrentEnemyHealth = Mathf.Clamp(newValue, 1f, CurrentEnemyMaxHealth);
            enemyHealthInput.text = CurrentEnemyHealth.ToString("F0");
        }
        else
        {
            enemyHealthInput.text = CurrentEnemyHealth.ToString("F0");
        }
    }
    
    void OnEnemyMaxHealthChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentEnemyMaxHealth = Mathf.Max(1f, newValue);
            enemyMaxHealthInput.text = CurrentEnemyMaxHealth.ToString("F0");
            
            // 如果当前生命值大于新的最大生命值，则调整
            if (CurrentEnemyHealth > CurrentEnemyMaxHealth)
            {
                CurrentEnemyHealth = CurrentEnemyMaxHealth;
                enemyHealthInput.text = CurrentEnemyHealth.ToString("F0");
            }
        }
        else
        {
            enemyMaxHealthInput.text = CurrentEnemyMaxHealth.ToString("F0");
        }
    }
    #endregion
    
    /// <summary>
    /// 返回上一页按钮点击事件
    /// </summary>
    void OnBackClick()
    {
        // 保存设置
        SaveSettings();
        
        // 返回主设置菜单
        if (waveSetPanel != null)
            waveSetPanel.SetActive(false);
            
        if (mainSetPanel != null)
            mainSetPanel.SetActive(true);
            
        Debug.Log("返回主设置菜单");
    }
    
    /// <summary>
    /// 重置为默认值
    /// </summary>
    public void ResetToDefaults()
    {
        CurrentSpawnInterval = defaultSpawnInterval;
        CurrentEnemiesPerWave = defaultEnemiesPerWave;
        CurrentAttackWaves = defaultAttackWaves;
        CurrentAvoidanceRadius = defaultAvoidanceRadius;
        CurrentEnemyHealth = defaultEnemyHealth;
        CurrentEnemyMaxHealth = defaultEnemyMaxHealth;
        
        UpdateInputFieldValues();
        SaveSettings();
        
        Debug.Log("波次设置已重置为默认值");
    }
    
    void OnDestroy()
    {
        // 移除输入框事件监听
        if (spawnIntervalInput != null)
            spawnIntervalInput.onEndEdit.RemoveAllListeners();
        if (enemiesPerWaveInput != null)
            enemiesPerWaveInput.onEndEdit.RemoveAllListeners();
        if (attackWavesInput != null)
            attackWavesInput.onEndEdit.RemoveAllListeners();
        if (avoidanceRadiusInput != null)
            avoidanceRadiusInput.onEndEdit.RemoveAllListeners();
        if (enemyHealthInput != null)
            enemyHealthInput.onEndEdit.RemoveAllListeners();
        if (enemyMaxHealthInput != null)
            enemyMaxHealthInput.onEndEdit.RemoveAllListeners();
            
        // 移除按钮事件监听
        if (backButton != null)
            backButton.onClick.RemoveAllListeners();
        if (resetButton != null)
            resetButton.onClick.RemoveAllListeners();
    }
}
