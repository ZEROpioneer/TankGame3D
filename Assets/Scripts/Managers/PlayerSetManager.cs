using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSetManager : MonoBehaviour
{
    [Header("移动设置")]
    public TMP_InputField moveSpeedInputField;      // 移动速度输入框
    public TMP_InputField rotateSpeedInputField;    // 旋转速度输入框
    
    [Header("射击设置")]
    public TMP_InputField shootSpeedInputField;     // 子弹速度输入框
    public TMP_InputField shootHealthInputField;  // 子弹生命周期输入框
    public TMP_InputField shootdamageInputField;     // 子弹伤害输入框
    
    [Header("生命值设置")]
    public TMP_InputField maxHealthInputField;      // 最大生命值输入框
    
    [Header("UI控制")]
    public Button confirmButton;        // 返回主菜单按钮
    public Button ResertButton;        // 重置默认值按钮
    public GameObject PlayerSetPanel;    // 玩家设置面板
    public GameObject SetPanel;    // 主设置菜单面板
    
    [Header("默认参数值")]
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float defaultRotateSpeed = 100f;
    [SerializeField] private float defaultShootSpeed = 20f;
    [SerializeField] private float shootHealthInput = 5f;
    [SerializeField] private float shootdamageInput = 1f;
    [SerializeField] private float defaultMaxHealth = 3f;
    
    // 当前参数值（供其他脚本访问）
    public static float CurrentMoveSpeed { get; private set; }
    public static float CurrentRotateSpeed { get; private set; }
    public static float CurrentShootSpeed { get; private set; }
    public static float CurrentHealthInput { get; private set; }
    public static float Currentshootdamage { get; private set; }
    public static float CurrentMaxHealth { get; private set; }
    
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
        CurrentMoveSpeed = PlayerPrefs.GetFloat("MoveSpeed", defaultMoveSpeed);
        CurrentRotateSpeed = PlayerPrefs.GetFloat("RotateSpeed", defaultRotateSpeed);
        CurrentShootSpeed = PlayerPrefs.GetFloat("ShootSpeed", defaultShootSpeed);
        CurrentHealthInput = PlayerPrefs.GetFloat("HealthInput", shootHealthInput);
        Currentshootdamage = PlayerPrefs.GetFloat("shootdamage", shootdamageInput);
        CurrentMaxHealth = PlayerPrefs.GetFloat("MaxHealth", defaultMaxHealth);
    }
    
    /// <summary>
    /// 保存设置到PlayerPrefs
    /// </summary>
    void SaveSettings()
    {
        PlayerPrefs.SetFloat("MoveSpeed", CurrentMoveSpeed);
        PlayerPrefs.SetFloat("RotateSpeed", CurrentRotateSpeed);
        PlayerPrefs.SetFloat("ShootSpeed", CurrentShootSpeed);
        PlayerPrefs.SetFloat("HealthInput", CurrentHealthInput);
        PlayerPrefs.SetFloat("shootdamage", Currentshootdamage);
        PlayerPrefs.SetFloat("MaxHealth", CurrentMaxHealth);
        PlayerPrefs.Save();
        
        Debug.Log("设置已保存");
    }
    
    /// <summary>
    /// 更新输入框数值显示
    /// </summary>
    void UpdateInputFieldValues()
    {
        if (moveSpeedInputField != null)
            moveSpeedInputField.text = CurrentMoveSpeed.ToString("F1");
            
        if (rotateSpeedInputField != null)
            rotateSpeedInputField.text = CurrentRotateSpeed.ToString("F0");
            
        if (shootSpeedInputField != null)
            shootSpeedInputField.text = CurrentShootSpeed.ToString("F1");
            
        if (shootHealthInputField != null)
            shootHealthInputField.text = CurrentHealthInput.ToString("F2");
            
        if (shootdamageInputField != null)
            shootdamageInputField.text = Currentshootdamage.ToString("F0");
            
        if (maxHealthInputField != null)
            maxHealthInputField.text = CurrentMaxHealth.ToString("F0");
        Debug.Log("MaxHealth Field Found? " + (maxHealthInputField != null));

    }
    
    /// <summary>
    /// 绑定输入框事件
    /// </summary>
    void BindInputFieldEvents()
    {
        if (moveSpeedInputField != null)
            moveSpeedInputField.onEndEdit.AddListener(OnMoveSpeedChanged);
            
        if (rotateSpeedInputField != null)
            rotateSpeedInputField.onEndEdit.AddListener(OnRotateSpeedChanged);
            
        if (shootSpeedInputField != null)
            shootSpeedInputField.onEndEdit.AddListener(OnShootSpeedChanged);
            
        if (shootHealthInputField != null)
            shootHealthInputField.onEndEdit.AddListener(OnshootHealthChanged);
            
        if (shootdamageInputField != null)
            shootdamageInputField.onEndEdit.AddListener(OnshootdamageChanged);
            
        if (maxHealthInputField != null)
            maxHealthInputField.onEndEdit.AddListener(OnMaxHealthChanged);
    }
    
    /// <summary>
    /// 绑定按钮事件
    /// </summary>
    void BindButtonEvents()
    {
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmClick);
        
        if (ResertButton != null)
            ResertButton.onClick.AddListener(ResetToDefaults);
    }
  
    #region 输入框值改变事件
    void OnMoveSpeedChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentMoveSpeed = Mathf.Max(0.1f, newValue); // 最小值限制
            moveSpeedInputField.text = CurrentMoveSpeed.ToString("F1");
        }
        else
        {
            // 输入无效，恢复默认值
            moveSpeedInputField.text = CurrentMoveSpeed.ToString("F1");
        }
    }
    
    void OnRotateSpeedChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentRotateSpeed = Mathf.Max(1f, newValue);
            rotateSpeedInputField.text = CurrentRotateSpeed.ToString("F0");
        }
        else
        {
            rotateSpeedInputField.text = CurrentRotateSpeed.ToString("F0");
        }
    }
    
    void OnShootSpeedChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentShootSpeed = Mathf.Max(0.1f, newValue);
            shootSpeedInputField.text = CurrentShootSpeed.ToString("F1");
        }
        else
        {
            shootSpeedInputField.text = CurrentShootSpeed.ToString("F1");
        }
    }
    
    void OnshootHealthChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentHealthInput = Mathf.Max(0.01f, newValue);
            shootHealthInputField.text = CurrentHealthInput.ToString("F2");
        }
        else
        {
            shootHealthInputField.text = CurrentHealthInput.ToString("F2");
        }
    }
    
    void OnshootdamageChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            Currentshootdamage = Mathf.Max(1f, newValue);
            shootdamageInputField.text = Currentshootdamage.ToString("F0");
        }
        else
        {
            shootdamageInputField.text = Currentshootdamage.ToString("F0");
        }
    }
    
    void OnMaxHealthChanged(string value)
    {
        if (float.TryParse(value, out float newValue))
        {
            CurrentMaxHealth = Mathf.Max(1f, newValue);
            maxHealthInputField.text = CurrentMaxHealth.ToString("F0");
        }
        else
        {
            maxHealthInputField.text = CurrentMaxHealth.ToString("F0");
        }
    }
    #endregion
    
    /// <summary>
    /// 返回主菜单按钮点击事件
    /// </summary>
    void OnConfirmClick()
    {
        // 保存设置
        SaveSettings();
        
        // 返回主设置菜单
        if (PlayerSetPanel != null)
            PlayerSetPanel.SetActive(false);
            
        if (SetPanel != null)
            SetPanel.SetActive(true);
            
        Debug.Log("返回主设置菜单");
    }
    
    /// <summary>
    /// 重置为默认值
    /// </summary>
    public void ResetToDefaults()
    {
        CurrentMoveSpeed = defaultMoveSpeed;
        CurrentRotateSpeed = defaultRotateSpeed;
        CurrentShootSpeed = defaultShootSpeed;
        CurrentHealthInput = shootHealthInput;
        Currentshootdamage = shootdamageInput;
        CurrentMaxHealth = defaultMaxHealth;
        
        UpdateInputFieldValues();
        SaveSettings();
        
        Debug.Log("设置已重置为默认值");
    }
    
    void OnDestroy()
    {
        // 移除输入框事件监听
        if (moveSpeedInputField != null)
            moveSpeedInputField.onEndEdit.RemoveAllListeners();
        if (rotateSpeedInputField != null)
            rotateSpeedInputField.onEndEdit.RemoveAllListeners();
        if (shootSpeedInputField != null)
            shootSpeedInputField.onEndEdit.RemoveAllListeners();
        if (shootHealthInputField != null)
            shootHealthInputField.onEndEdit.RemoveAllListeners();
        if (shootdamageInputField != null)
            shootdamageInputField.onEndEdit.RemoveAllListeners();
        if (maxHealthInputField != null)
            maxHealthInputField.onEndEdit.RemoveAllListeners();
        if (confirmButton != null)
            confirmButton.onClick.RemoveAllListeners();
    }
}
