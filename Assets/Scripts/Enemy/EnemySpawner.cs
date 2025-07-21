using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成设置")]
    public GameObject enemyTankPrefab;
    public Transform[] spawnPoints;
    public int maxEnemies = 5;
    public float spawnInterval = 3f;
    public int totalEnemiesPerWave = 10;
    
    [Header("波次设置")]
    public int currentWave = 1;
    public float waveInterval = 10f;
    public int ADDEnemies = 2;  // 递增敌人数
    
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesSpawnedThisWave = 0;
    private float nextSpawnTime = 0f;
    private bool waveActive = true;
    
    // UI 管理器引用
    private WaveUIManager waveUIManager;
    
    void Start()
    {
        // 如果没有设置生成点，创建默认生成点
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            CreateDefaultSpawnPoints();
        }
        
        // 查找 UI 管理器
        waveUIManager = FindObjectOfType<WaveUIManager>();
        
        nextSpawnTime = Time.time + 2f; // 游戏开始2秒后开始生成
    }
    
    void Update()
    {
        // 清理已死亡的敌人引用
        activeEnemies.RemoveAll(enemy => enemy == null);
        
        if (waveActive && Time.time >= nextSpawnTime)
        {
            if (activeEnemies.Count < maxEnemies && enemiesSpawnedThisWave < totalEnemiesPerWave)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
        
        // 检查波次是否结束
        if (enemiesSpawnedThisWave >= totalEnemiesPerWave && activeEnemies.Count == 0)
        {
            StartNextWave();
        }
    }
    
    void SpawnEnemy()
    {
        if (enemyTankPrefab == null || spawnPoints.Length == 0) return;
        
        // 随机选择生成点
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // 检查生成点是否被占用
        Collider[] overlapping = Physics.OverlapSphere(spawnPoint.position, 2f);
        foreach (Collider col in overlapping)
        {
            if (col.CompareTag("Enemy") || col.CompareTag("Player"))
            {
                return; // 生成点被占用，跳过这次生成
            }
        }
        
        // 生成敌人坦克
        GameObject newEnemy = Instantiate(enemyTankPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(newEnemy);
        enemiesSpawnedThisWave++;
        
        Debug.Log($"生成敌人坦克，当前波次：{currentWave}，已生成：{enemiesSpawnedThisWave}/{totalEnemiesPerWave}");
    }
    
    void CreateDefaultSpawnPoints()
    {
        // 创建默认生成点
        GameObject spawnPointsParent = new GameObject("SpawnPoints");
        spawnPointsParent.transform.position = Vector3.zero;
        
        Transform[] defaultSpawnPoints = new Transform[3];
        
        // 左侧生成点
        GameObject leftSpawn = new GameObject("SpawnPoint_Left");
        leftSpawn.transform.position = new Vector3(-10, 0.5f, 20);
        leftSpawn.transform.SetParent(spawnPointsParent.transform);
        defaultSpawnPoints[0] = leftSpawn.transform;
        
        // 中央生成点
        GameObject centerSpawn = new GameObject("SpawnPoint_Center");
        centerSpawn.transform.position = new Vector3(0, 0.5f, 20);
        centerSpawn.transform.SetParent(spawnPointsParent.transform);
        defaultSpawnPoints[1] = centerSpawn.transform;
        
        // 右侧生成点
        GameObject rightSpawn = new GameObject("SpawnPoint_Right");
        rightSpawn.transform.position = new Vector3(10, 0.5f, 20);
        rightSpawn.transform.SetParent(spawnPointsParent.transform);
        defaultSpawnPoints[2] = rightSpawn.transform;
        
        spawnPoints = defaultSpawnPoints;
    }
    
    void StartNextWave()
    {
        int completedWave = currentWave;
        currentWave++;
        enemiesSpawnedThisWave = 0;
        waveActive = false;
        
        Debug.Log($"波次 {currentWave} 开始！");
        
        // 通知 UI 管理器波次完成
        if (waveUIManager != null)
        {
            waveUIManager.ShowWaveComplete(completedWave);
        }
        
        // 增加难度
        totalEnemiesPerWave += ADDEnemies;
        spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f);
        
        // 延迟开始下一波
        Invoke("StartWave", waveInterval);
    }
    
    void StartWave()
    {
        waveActive = true;
        nextSpawnTime = Time.time + 1f;
    }
    
    public void OnEnemyDestroyed()
    {
        // 当敌人被摧毁时调用此方法
        // 可以在这里添加得分逻辑
    }
    
    // 为 UI 提供的公共方法
    public int GetActiveEnemyCount()
    {
        return activeEnemies.Count;
    }
    
    public int GetEnemiesSpawnedThisWave()
    {
        return enemiesSpawnedThisWave;
    }
    
    public int GetTotalEnemiesPerWave()
    {
        return totalEnemiesPerWave;
    }
    
    public bool IsWaveActive()
    {
        return waveActive;
    }
}
