using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // 涉及 新版的部分UI控件 需要调用

public class WaveUIManager : MonoBehaviour
{
    [Header("UI 组件")]
    // 当前波次
    public TextMeshProUGUI waveText;
    // 当前敌人数
    public TextMeshProUGUI enemiesText;
    // 当前波次生成了多少敌人
    public TextMeshProUGUI progressText;
    // 进度条（波次产生进度）
    public Slider progressSlider;
    // 血量条
    public Slider HealthSlider;
    // 玩家
    public HealthSystem Health;
    

    [Header("波次间隔提示")]
    public GameObject waveCompletePanel;
    public TextMeshProUGUI waveCompleteText;
    public TextMeshProUGUI nextWaveCountdownText;
    
    [Header("动画设置")]
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;
    
    private EnemySpawner enemySpawner;
    private CanvasGroup waveCompleteCanvasGroup;
    private float countdownTimer = 0f;
    private bool isCountingDown = false;
    
    
    void Start()
    {
        // 查找 EnemySpawner 组件
        // 自动查找场景中的 EnemySpawner 实例，用于获取波次、敌人数、倒计时等信息
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("找不到 EnemySpawner 组件！");
            return;
        }
        
        // 设置波次完成面板
        if (waveCompletePanel != null)
        {
            waveCompleteCanvasGroup = waveCompletePanel.GetComponent<CanvasGroup>();
            if (waveCompleteCanvasGroup == null)
            {
                waveCompleteCanvasGroup = waveCompletePanel.AddComponent<CanvasGroup>();
            }
            waveCompletePanel.SetActive(false);
        }
        
        // 初始化 UI
        UpdateWaveUI();

    }
    
    void Update()
    {
        if (enemySpawner != null)
        {
            UpdateWaveUI();
        }
        

        
        // 处理波次间隔倒计时
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                isCountingDown = false;
                HideWaveCompletePanel();
            }
            else
            {
                UpdateCountdownText();
            }
        }
    }
   
    void UpdateWaveUI()
    {
        // 更新波次文本
        if (waveText != null)
        {
            // 获取当前波次 赋值给 waveText中的 text 字段
            waveText.text = $"波次: {enemySpawner.currentWave}";
        }
        
        // 更新敌人数量文本
        if (enemiesText != null)
        {
            int activeCount = enemySpawner.GetActiveEnemyCount();
            enemiesText.text = $"敌人: {activeCount}";
        }
        // 更新血量
        if (Health != null)
        {
            float currentHealth = Health.currentHealth;
            float maxHealth = Health.maxHealth;
            // Slider.value 是进度条当前值，范围是 [0, 1]
            HealthSlider.value = (float)currentHealth / maxHealth;
        }
        
        // 更新进度文本和滑块
        if (progressText != null)
        {
            int spawned = enemySpawner.GetEnemiesSpawnedThisWave();
            int total = enemySpawner.totalEnemiesPerWave;
            progressText.text = $"进度: {spawned}/{total}";
            
            // 更新进度条
            if (progressSlider != null)
            {
                // Slider.value 是进度条当前值，范围是 [0, 1]
                progressSlider.value = (float)spawned / total;
            }
        }
    }
    

    
    public void ShowWaveComplete(int completedWave)
    {
        if (waveCompletePanel != null && waveCompleteText != null)
        {
            waveCompletePanel.SetActive(true);
            waveCompleteText.text = $"波次 {completedWave} 完成！";
            
            // 开始倒计时
            countdownTimer = enemySpawner.waveInterval;
            isCountingDown = true;
            
            // 淡入动画
            if (waveCompleteCanvasGroup != null)
            {
                StartCoroutine(FadeInPanel());
            }
        }
    }
    
    void UpdateCountdownText()
    {
        if (nextWaveCountdownText != null)
        {
            int seconds = Mathf.CeilToInt(countdownTimer);
            nextWaveCountdownText.text = $"下一波开始倒计时: {seconds}秒";
        }
    }
    
    void HideWaveCompletePanel()
    {
        if (waveCompletePanel != null)
        {
            if (waveCompleteCanvasGroup != null)
            {
                StartCoroutine(FadeOutPanel());
            }
            else
            {
                waveCompletePanel.SetActive(false);
            }
        }
    }
    
    System.Collections.IEnumerator FadeInPanel()
    {
        float timer = 0f;
        waveCompleteCanvasGroup.alpha = 0f;
        
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            waveCompleteCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
            yield return null;
        }
        
        waveCompleteCanvasGroup.alpha = 1f;
    }
    
    System.Collections.IEnumerator FadeOutPanel()
    {
        float timer = 0f;
        float startAlpha = waveCompleteCanvasGroup.alpha;
        
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            waveCompleteCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeOutDuration);
            yield return null;
        }
        
        waveCompleteCanvasGroup.alpha = 0f;
        waveCompletePanel.SetActive(false);
    }
}
