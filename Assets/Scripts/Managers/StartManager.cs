using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [Header("UI按钮引用")]
    public Button startGameButton;      // 开始游戏按钮
    public Button exitGameButton;       // 退出游戏按钮
    public Button settingsButton;       // 游戏设置按钮
    public Button otherButton;          // 其他按钮
    
    [Header("其他界面面板")]
    public GameObject settingsPanel;     // 游戏设置面板
    public GameObject mainMenuPanel;     // 主菜单面板（自身）
    
    [Header("场景设置")]
    public string gameSceneName = "GameScene";  // 游戏场景名称
    
    void Start()
    {
        // 暂停游戏，等待玩家点击开始
        Time.timeScale = 0f;
        // 绑定按钮事件
        BindButtonEvents();
        
        // 确保主菜单面板显示，其他面板隐藏
        ShowMainMenu();
    }
    
    /// <summary>
    /// 绑定按钮点击事件
    /// </summary>
    void BindButtonEvents()  //将 方法 与 实际按钮绑定
    {
        if (startGameButton != null)
            startGameButton.onClick.AddListener(OnStartGameClick);
            
        if (exitGameButton != null)
            exitGameButton.onClick.AddListener(OnExitGameClick);
            
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsClick);
            
        if (otherButton != null)
            otherButton.onClick.AddListener(OnOtherClick);
    }
    
    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    public void OnStartGameClick()
    {
        Debug.Log("开始游戏被点击");
        // 隐藏主菜单UI
        mainMenuPanel.SetActive(false);

        // 开始游戏，恢复时间流逝
        Time.timeScale = 1f;
        
        // 方式一：直接加载游戏场景
        //if (!string.IsNullOrEmpty(gameSceneName))
        //{
            //SceneManager.LoadScene(gameSceneName);
        //}
        
        // 方式二：如果游戏在同一场景，可以隐藏菜单显示游戏UI
        // UIManager.Instance.ShowPanel("GamePlay");
        // GameManager.Instance.StartGame();
    }
    
    /// <summary>
    /// 退出游戏按钮点击事件
    /// </summary>
    public void OnExitGameClick()
    {
        Debug.Log("退出游戏被点击");
        ExitGame();
    }
    
    /// <summary>
    /// 游戏设置按钮点击事件
    /// </summary>
    public void OnSettingsClick()
    {
        Debug.Log("游戏设置被点击");
        
        if (settingsPanel != null)
        {
            // 隐藏主菜单，显示设置界面
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("设置面板引用为空！请在Inspector中设置。");
        }
    }
    
    /// <summary>
    /// 其他按钮点击事件
    /// </summary>
    public void OnOtherClick()
    {
        Debug.Log("其他按钮被点击");
        
        // 由于这个界面还没准备好，现在只是打印日志
        // 您可以在这里添加临时功能或者显示"敬请期待"的提示
        ShowTempMessage("该功能正在开发中，敬请期待！");
    }
    
    /// <summary>
    /// 显示主菜单（从其他界面返回时调用）
    /// </summary>
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
            
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    /// <summary>
    /// 显示临时消息（用于未完成的功能）
    /// </summary>
    void ShowTempMessage(string message)
    {
        // 这里可以显示一个临时的提示框
        // 如果您有MessageBox或Toast组件的话
        Debug.Log($"提示: {message}");
        
        // 简单的临时实现：在控制台显示消息
        // 后续可以替换为UI提示框
    }
    
    /// <summary>
    /// 退出游戏（如果需要的话）
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("退出游戏");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    void OnDestroy()
    {
        // 移除按钮事件监听，防止内存泄漏
        if (startGameButton != null)
            startGameButton.onClick.RemoveAllListeners();
            
        if (exitGameButton != null)
            exitGameButton.onClick.RemoveAllListeners();
            
        if (settingsButton != null)
            settingsButton.onClick.RemoveAllListeners();
            
        if (otherButton != null)
            otherButton.onClick.RemoveAllListeners();
    }
}