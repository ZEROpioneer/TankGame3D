using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetManager : MonoBehaviour
{
    [Header("UI按钮引用")]
    public Button playerButton;      // 玩家设置按钮
    public Button GameButton;       // 游戏设置按钮
    public Button EnemyButton;       // 敌人设置按钮
    public Button ReturnButton;       // 返回主菜单按钮
    
    [Header("其他界面面板")]
    public GameObject PlayerSetPanel;     // 玩家设置面板
    public GameObject EnemySetPanel;     // 敌人设置面板
    public GameObject GameSetPanel;     // 游戏设置面板
    public GameObject SetPanel;     // 主设置面板（自身）
    public GameObject MainPanel;     // 开始游戏面板
    
    void Start()
    {
        // 暂停游戏，等待玩家点击开始
        Time.timeScale = 0f;
        // 绑定按钮事件
        BindButtonEvents();
        
        // 确保主菜单面板显示，其他面板隐藏
        ShowSetPanel();
    }
    
    /// <summary>
    /// 绑定按钮点击事件
    /// </summary>
    void BindButtonEvents()  //将 方法 与 实际按钮绑定
    {
        if (playerButton != null)
            playerButton.onClick.AddListener(OnplayerButtonClick);
            
        if (GameButton != null)
            GameButton.onClick.AddListener(OnGameButtonClick);
            
        if (EnemyButton != null)
            EnemyButton.onClick.AddListener(OnEnemyButtonClick);
        
        if (ReturnButton != null)
            ReturnButton.onClick.AddListener(OnReturnButtonClick);
        
    }
    
    /// <summary>
    /// 玩家设置按钮点击事件
    /// </summary>
    public void OnplayerButtonClick()
    {
        Debug.Log("进入玩家设置界面");
        // 隐藏主菜单UI
        SetPanel.SetActive(false);
        // 开启玩家设置界面
        PlayerSetPanel.SetActive(true);
 
    }
    
    /// <summary>
    /// 打开游戏设置界面按钮点击事件
    /// </summary>
    public void OnGameButtonClick()
    {
        Debug.Log("进入游戏设置界面");
        // 隐藏主菜单UI
        SetPanel.SetActive(false);
        // 开启游戏设置界面
        GameSetPanel.SetActive(true);

    }
    
    /// <summary>
    /// 敌人设置按钮点击事件
    /// </summary>
    public void OnEnemyButtonClick()
    {
        Debug.Log("进入敌人设置界面");
        // 隐藏主菜单UI
        SetPanel.SetActive(false);
        // 开启敌人设置界面
        EnemySetPanel.SetActive(true);
    }
    
    public void OnReturnButtonClick()
    {
        Debug.Log("返回主菜单");
        // 隐藏主菜单UI
        SetPanel.SetActive(false);
        // 开启主界面
        MainPanel.SetActive(true);
    }
    
    
    /// <summary>
    /// 显示主菜单（从其他界面返回时调用）
    /// </summary>
    public void ShowSetPanel()
    {
        if (SetPanel != null)
            SetPanel.SetActive(true);
        
    }

}
