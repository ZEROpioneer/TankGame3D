using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // 涉及 新版的部分UI控件 需要调用


public class OverManager : MonoBehaviour
{
    [Header("UI组件")] 
    public TextMeshProUGUI EnemyCount;  // 敌人数
    public TextMeshProUGUI WaveCount;  // 波次数
    public Button OverQuitButton;  // 退出游戏
    public Button OverReGameButton;  // 重新开始
    public GameObject OverPanel;  // 结束界面 
    [Header("玩家血量检测")]
    public HealthSystem playerHealthSystem;  // 玩家血量组件的引用
    
    private EnemySpawner enemySpawner;
    private bool isGameOver = false;  // 游戏是否结束标志

    void Start()
    {
        // 确保暂停面板初始状态为隐藏
        if (OverPanel != null)
        {
            // 如果存在 OverPanel ，那么就让他失活
            OverPanel.SetActive(false);
        }
        
        // 查找 EnemySpawner 组件
        // 自动查找场景中的 EnemySpawner 实例，用于获取波次、敌人数、倒计时等信息
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("找不到 EnemySpawner 组件！");
            return;
        }
        // 设置按钮事件
        if (OverQuitButton != null)
        {
            //退出游戏
            // 为button中的 onclick 字段 ，添加事件（点击按钮时 就调用()中的函数）
            OverQuitButton.onClick.AddListener(OverQuitGame);
        }
        
        if (OverReGameButton != null)
        {
            // 重新开始
            OverReGameButton.onClick.AddListener(RestartGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 检查玩家是否还存在，如果不存在就结束游戏
        if (!isGameOver)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.Log("玩家对象被击败，触发游戏结束！");
                GameOver();
            }
        }
    }
    // 游戏结束方法
    void GameOver()
    {
        isGameOver = true;  // 设置游戏结束标志
        
        // 暂停游戏
        Time.timeScale = 0f;
        
        // 显示结束面板
        if (OverPanel != null)
        {
            OverPanel.SetActive(true);
        }
        
        // 更新UI显示
        UpdateOverUI();
        
        Debug.Log("游戏结束！");
    }
    
    void UpdateOverUI()
    {
        // 获取波次文本
        if (WaveCount != null)
        {
            // 获取当前波次 赋值给 WaveCount中的 text 字段
            WaveCount.text = $"当前波次：{enemySpawner.currentWave}";
        }
        
        // 计算敌人数量 输出文本
        if (EnemyCount != null)
        {
            // 当前波次已经击败数 = 该波次敌人生成数 - 当前存活敌人数
            int ThisWaveCount = enemySpawner.GetEnemiesSpawnedThisWave() - enemySpawner.GetActiveEnemyCount();
            int LastWaveCount = (enemySpawner.currentWave - 1) * 10 +
                                (enemySpawner.currentWave - 2) * (enemySpawner.currentWave-1);
            EnemyCount.text = $"击败敌人: {LastWaveCount+ThisWaveCount}";
        }
        
    }
    // 退出游戏方法
    public void OverQuitGame()
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
    // 重新开始方法
    public void RestartGame()
    {
        Time.timeScale = 1f;
        // 重启场景
        SceneManager.LoadScene("GameScene"); 
    }
}
