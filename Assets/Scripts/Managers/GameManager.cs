using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("游戏状态")]
    public static GameManager Instance;
    public bool isGameActive = true;
    public bool isGamePaused = false;
    
    [Header("得分系统")]
    public int score = 0;
    public int lives = 3;
    public int enemyKillScore = 100;
    
    [Header("UI引用")]
    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject victoryPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public Button pauseButton;
    public Button resumeButton;
    
    [Header("游戏对象引用")]
    public GameObject playerTank;
    public GameObject playerBase;
    public Transform playerSpawnPoint;
    
    [Header("音效")]
    public AudioSource audioSource;
    public AudioClip enemyDestroySound;
    public AudioClip playerHitSound;
    public AudioClip gameOverSound;
    public AudioClip victorySound;
    
    private EnemySpawner enemySpawner;
    private int currentWave = 1;
    
    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        audioSource = GetComponent<AudioSource>();
        
        // 设置按钮事件
        if (restartButton) restartButton.onClick.AddListener(RestartGame);
        if (mainMenuButton) mainMenuButton.onClick.AddListener(LoadMainMenu);
        if (pauseButton) pauseButton.onClick.AddListener(PauseGame);
        if (resumeButton) resumeButton.onClick.AddListener(ResumeGame);
        
        UpdateUI();
    }
    
    void Update()
    {
        // 暂停游戏快捷键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
        
        // 播放得分音效
        if (audioSource && enemyDestroySound)
        {
            audioSource.PlayOneShot(enemyDestroySound);
        }
    }
    
    public void PlayerHit()
    {
        lives--;
        UpdateUI();
        
        // 播放受伤音效
        if (audioSource && playerHitSound)
        {
            audioSource.PlayOneShot(playerHitSound);
        }
        
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // 重生玩家
            StartCoroutine(RespawnPlayer());
        }
    }
    
    public void BaseDestroyed()
    {
        GameOver();
    }
    
    public void WaveCompleted()
    {
        currentWave++;
        UpdateUI();
        
        // 检查胜利条件
        if (currentWave > 5) // 假设5波后胜利
        {
            Victory();
        }
    }
    
    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (livesText) livesText.text = "Lives: " + lives;
        if (waveText) waveText.text = "Wave: " + currentWave;
    }
    
    void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0f;
        
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (audioSource && gameOverSound)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        
        Debug.Log("游戏结束！");
    }
    
    void Victory()
    {
        isGameActive = false;
        Time.timeScale = 0f;
        
        if (victoryPanel)
        {
            victoryPanel.SetActive(true);
        }
        
        if (audioSource && victorySound)
        {
            audioSource.PlayOneShot(victorySound);
        }
        
        Debug.Log("胜利！");
    }
    
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        
        if (pausePanel)
        {
            pausePanel.SetActive(true);
        }
    }
    
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // 需要创建主菜单场景
    }
    
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);
        
        if (playerTank && playerSpawnPoint)
        {
            playerTank.transform.position = playerSpawnPoint.position;
            playerTank.transform.rotation = playerSpawnPoint.rotation;
            
            // 重置玩家生命值
            HealthSystem playerHealth = playerTank.GetComponent<HealthSystem>();
            if (playerHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
            }
        }
    }
}
