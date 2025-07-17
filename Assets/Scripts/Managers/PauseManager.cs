using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("暂停UI")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button quitButton;
    
    [Header("暂停设置")]
    // 设置暂停键 默认为 Esc键
    public KeyCode pauseKey = KeyCode.Escape;
    // 记录当前游戏 是否处于暂停状态
    private bool isPaused = false;
    
    void Start()
    {
        // 确保暂停面板初始状态为隐藏
        if (pausePanel != null)
        {
            // 如果存在 pausePanel ，那么就让他失活
            pausePanel.SetActive(false);
        }
        
        // 设置按钮事件
        if (resumeButton != null)
        {
            // 为button中的 onclick 字段 ，添加事件（点击按钮时 就调用()中的函数）
            resumeButton.onClick.AddListener(ResumeGame);
        }
        
        if (quitButton != null)
        {
            // 同上
            quitButton.onClick.AddListener(QuitGame);
        }
    }
    
    void Update()
    {
        // 检测暂停键
        // 检测是否 按下 对应按键
        // ()中参数 为 要检测的按键
        // GetKeyDown 按下触发  GetKey 按住期间触发（每帧） GetKeyUp 松开瞬间触发
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)  // 判断 isPaused 变量，查看是否为暂停状态
            {
                ResumeGame();  // 如果为暂停状态，调用函数 恢复游戏
            }
            else
            {
                PauseGame();  // 如果为进行状态，调用函数 暂停游戏
            }
        }
    }
    
    public void PauseGame()  // 暂停游戏的方法
    {
        isPaused = true;  // 更改代表游戏状态的 变量
        // Unity 中所有使用 Time.deltaTime 控制的逻辑
        // 如动画、移动、计时器）都会乘以 timeScale；
        // 所以 timeScale设置为 0，就相当于暂停时间
        Time.timeScale = 0f; // 暂停游戏时间
        
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        
        // 显示鼠标光标
        // 控制鼠标光标是否被限制在游戏窗口内以及是否可见
        // None 值：	鼠标可以自由移出游戏窗口 光标保持可见（除非单独设置
        Cursor.lockState = CursorLockMode.None; 
        // 直接控制鼠标光标的显示/隐藏状态
        // true	显示系统鼠标光标
        Cursor.visible = true;  
        
        Debug.Log("游戏已暂停");
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复游戏时间
        
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);  // 失活 暂停游戏面板
        }
        
        // 隐藏鼠标光标（如果需要的话）
        Cursor.lockState = CursorLockMode.Locked;  //鼠标锁定在窗口中心位置光标自动隐藏
        Cursor.visible = false;  //隐藏系统鼠标光标
        
        Debug.Log("游戏已恢复");
    }
    
    public void QuitGame()
    {
        Debug.Log("退出游戏");
        
        // 在编辑器中
        #if UNITY_EDITOR  // 判断是否是在 unity编译器 环境中
        // EditorApplication.isPlaying 控制编辑器的播放状态
        UnityEditor.EditorApplication.isPlaying = false;  //停止播放模式
        #else
        // 在构建版本中
        Application.Quit();  // 关闭应用程序
        #endif
    }
    
    public bool IsPaused()
    {
        return isPaused;
    }
}
