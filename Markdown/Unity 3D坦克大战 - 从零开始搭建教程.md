# Unity 3D坦克大战 - 从零开始搭建教程

## 第一步：创建Unity项目

### 1.1 新建项目

1. 打开Unity Hub
2. 点击"New Project"
3. 选择"3D"模板
4. 项目名称：`TankBattle3D`
5. 选择保存位置
6. 点击"Create"

### 1.2 初始设置

```
Window → Package Manager → 安装以下包：
- Input System (新输入系统，可选)
- ProBuilder (快速建模，可选)
```

## 第二步：场景环境搭建

### 2.1 创建基础地面

```
1. 右键Hierarchy → 3D Object → Plane
2. 重命名为"Ground"
3. 设置Transform：
   - Position: (0, 0, 0)
   - Scale: (5, 1, 5)  // 创建50x50的地面
```

### 2.2 创建边界墙壁

```
创建4面墙壁围成战场边界：

墙壁1 (北墙):
- 3D Object → Cube
- 名称："Wall_North"
- Position: (0, 0.5, 25)
- Scale: (50, 1, 1)

墙壁2 (南墙):
- 名称："Wall_South"  
- Position: (0, 0.5, -25)
- Scale: (50, 1, 1)

墙壁3 (东墙):
- 名称："Wall_East"
- Position: (25, 0.5, 0)
- Scale: (1, 1, 50)

墙壁4 (西墙):
- 名称："Wall_West"
- Position: (-25, 0.5, 0)
- Scale: (1, 1, 50)
```

### 2.3 添加内部障碍物（按经典坦克大战布局）

#### 创建砖块障碍物组织结构

```
在Hierarchy中创建父对象：
- 右键 → Create Empty
- 名称："BrickWalls"
- Position: (0, 0, 0)
```

#### 上半部分砖墙（垂直墙）

```
上排左侧砖墙群：
砖墙1: Position(-18, 0.5, 15), Scale(2, 1, 8)
砖墙2: Position(-12, 0.5, 15), Scale(2, 1, 8)
砖墙3: Position(-6, 0.5, 15), Scale(2, 1, 8)
砖墙4: Position(0, 0.5, 15), Scale(2, 1, 4)
砖墙5: Position(6, 0.5, 15), Scale(2, 1, 8)
砖墙6: Position(12, 0.5, 15), Scale(2, 1, 8)
砖墙7: Position(18, 0.5, 15), Scale(2, 1, 8)
```

#### 中间水平砖墙

```
中间左侧: Position(-12, 0.5, 5), Scale(8, 1, 2)
中间中央: Position(0, 0.5, 5), Scale(8, 1, 2)
中间右侧: Position(12, 0.5, 5), Scale(8, 1, 2)
```

#### 下半部分复杂结构

```
下排左侧群：
砖墙8: Position(-18, 0.5, -5), Scale(2, 1, 8)
砖墙9: Position(-12, 0.5, -5), Scale(2, 1, 8)
砖墙10: Position(-6, 0.5, -5), Scale(2, 1, 4)

下排右侧群：
砖墙11: Position(6, 0.5, -5), Scale(2, 1, 4)
砖墙12: Position(12, 0.5, -5), Scale(2, 1, 8)
砖墙13: Position(18, 0.5, -5), Scale(2, 1, 8)

中央复杂结构：
砖墙14: Position(0, 0.5, -10), Scale(6, 1, 2)
砖墙15: Position(-3, 0.5, -15), Scale(2, 1, 4)
砖墙16: Position(3, 0.5, -15), Scale(2, 1, 4)
```

#### 四角钢墙（不可破坏）

```
左上角钢墙: Position(-20, 0.5, 8), Scale(2, 1, 2)
右上角钢墙: Position(20, 0.5, 8), Scale(2, 1, 2)
左下角钢墙: Position(-20, 0.5, -8), Scale(2, 1, 2)
右下角钢墙: Position(20, 0.5, -8), Scale(2, 1, 2)
```

#### 基地保护墙

```
基地上方: Position(0, 0.5, -20), Scale(6, 1, 2)
基地左侧: Position(-3, 0.5, -22), Scale(2, 1, 2)
基地右侧: Position(3, 0.5, -22), Scale(2, 1, 2)
```

## 第三步：设置材质和颜色

### 3.1 创建材质文件夹

```
Project窗口 → 右键 → Create → Folder
命名为"Materials"
```

### 3.2 创建基础材质

```
在Materials文件夹中创建：

地面材质:
- 右键 → Create → Material
- 名称："Ground_Material"
- Albedo颜色：黑色 (0.1, 0.1, 0.1)

边界墙材质:
- 名称："Wall_Material"
- Albedo颜色：深灰色 (0.3, 0.3, 0.3)

砖墙材质:
- 名称："BrickWall_Material"
- Albedo颜色：砖红色 (0.8, 0.4, 0.2)

钢墙材质:
- 名称："SteelWall_Material"
- Albedo颜色：银灰色 (0.7, 0.7, 0.7)

基地材质:
- 名称："Base_Material"
- Albedo颜色：金黄色 (0.9, 0.8, 0.2)
```

### 3.3 应用材质

```
将材质拖拽到对应的GameObject上：
- Ground → Ground_Material
- 所有边界Wall → Wall_Material  
- 所有砖墙 → BrickWall_Material
- 四角钢墙 → SteelWall_Material
- 基地相关 → Base_Material
```

## 第三步补充：创建基地

### 3.4 创建玩家基地

```
玩家基地（底部中央）:
- 3D Object → Cube
- 名称："PlayerBase"
- Position: (0, 0.5, -24)
- Scale: (2, 1, 2)
- 应用Base_Material材质
```

## 第四步：创建坦克对象

### 4.1 玩家坦克

```
1. 创建空物体：
   - 右键Hierarchy → Create Empty
   - 名称："PlayerTank"
   - Position: (0, 0, -20)

2. 创建坦克身体：
   - 右键PlayerTank → 3D Object → Cube
   - 名称："TankBody"
   - Position: (0, 0.5, 0)
   - Scale: (2, 1, 3)

3. 创建坦克炮塔：
   - 右键PlayerTank → 3D Object → Cube
   - 名称："TankTurret"
   - Position: (0, 1, 0)
   - Scale: (1.5, 0.5, 1.5)

4. 创建炮管：
   - 右键TankTurret → 3D Object → Cube
   - 名称："TankBarrel"
   - Position: (0, 0, 1)
   - Scale: (0.2, 0.2, 2)
```

### 4.2 创建玩家坦克材质

```
Materials文件夹中创建：
- 名称："PlayerTank_Material"
- Albedo颜色：蓝色 (0.2, 0.4, 0.8)
- 应用到PlayerTank的所有子对象
```

### 4.3 设置火力点

```
在TankBarrel下创建空物体：
- 名称："FirePoint"
- Position: (0, 0, 1)  // 炮管前端
- 这个位置将用于生成子弹
```

## 第五步：摄像机设置

### 5.1 摄像机跟随设置

```
选择Main Camera：
- Position: (0, 15, -30)
- Rotation: (30, 0, 0)
- 这样设置可以俯视整个战场
```

### 5.2 创建摄像机跟随脚本（可选）

```
后续可以添加脚本让摄像机跟随玩家坦克移动
```

## 第六步：添加光照

### 6.1 设置方向光

```
选择Directional Light：
- Rotation: (50, -30, 0)
- Intensity: 1
- Color: 白色
```

### 6.2 添加环境光

```
Window → Rendering → Lighting
Environment选项卡：
- Environment Lighting → Source: Color
- Ambient Color: 浅灰色
- Intensity: 0.3
```

## 第七步：创建预制体

### 7.1 创建Prefabs文件夹

```
Project窗口 → 右键 → Create → Folder
命名为"Prefabs"
```

### 7.2 创建玩家坦克预制体

```
将PlayerTank从Hierarchy拖拽到Prefabs文件夹
这样就创建了玩家坦克的预制体
```

## 第八步：设置碰撞层级

### 8.1 创建自定义层级

```
右上角Layers → Edit Layers → User Layer：
- User Layer 8: Player
- User Layer 9: Enemy  
- User Layer 10: Bullet
- User Layer 11: Wall
```

### 8.2 分配层级

```
- PlayerTank → Layer: Player
- 所有边界Wall → Layer: Wall
- 所有砖墙(BrickWall) → Layer: Wall
- 四角钢墙 → Layer: Wall
- PlayerBase → Layer: Player
```

### 8.3 设置碰撞规则

```
Edit → Project Settings → Physics → Layer Collision Matrix
- Player 不与 Player 碰撞
- Bullet 与 Wall、Enemy、Player 碰撞
- Enemy 不与 Enemy 碰撞
```

## 第九步：NavMesh设置（为AI准备）

### 9.1 标记导航区域

```
选择Ground：
- 右上角Static → 勾选Navigation Static

选择所有墙壁和障碍物：
- 边界墙、砖墙、钢墙 → Static → 勾选Navigation Static  
- Navigation Area → Not Walkable

选择PlayerBase：
- Static → 勾选Navigation Static
- Navigation Area → Not Walkable
```

### 9.2 烘焙NavMesh

```
Window → AI → Navigation
Bake选项卡 → 点击"Bake"
你会看到地面出现蓝色网格，这就是可行走区域
```

## 第十步：项目结构整理

### 10.1 创建完整文件夹结构

```
Assets/
├── Materials/
├── Prefabs/
├── Scripts/        (后续创建)
├── Scenes/
└── Audio/          (后续添加)
```

### 10.2 保存场景

```
File → Save Scene As → "GameScene"
```

## 检查清单

完成以上步骤后，你的场景应该包含：

- [x] 50x50的黑色地面
- [x] 四面边界墙
- [x] 经典坦克大战布局的砖墙群
- [x] 四角不可破坏的钢墙
- [x] 底部中央的玩家基地
- [x] 基地周围的保护墙
- [x] 一个完整的玩家坦克模型
- [x] 合适的摄像机角度
- [x] 基础光照设置
- [x] NavMesh已烘焙
- [x] 材质已应用（砖红色砖墙，银灰色钢墙）

## 额外建议

### 创建敌人生成点

```
在场景上方创建3个敌人生成点：
- 左侧生成点: Position(-10, 0.5, 20)
- 中央生成点: Position(0, 0.5, 20)  
- 右侧生成点: Position(10, 0.5, 20)

这些位置留空，后续用于敌人坦克生成
```

## 下一步

环境搭建完成后，你可以开始：

1. 添加玩家坦克控制脚本
2. 创建子弹预制体
3. 实现射击系统
4. 添加敌人坦克和AI

现在你已经有了一个完整的3D坦克战场环境！

# Unity 3D坦克大战 - 玩家控制与射击系统

## 第一步：创建脚本文件夹结构

### 1.1 创建Scripts文件夹

```
Project窗口 → 右键 → Create → Folder
命名为"Scripts"

在Scripts文件夹中创建子文件夹：
- Player (玩家相关脚本)
- Enemy (敌人相关脚本)  
- Weapons (武器相关脚本)
- Managers (管理器脚本)
```

## 第二步：创建玩家坦克控制脚本

### 2.1 创建TankController脚本

```
在Scripts/Player文件夹中：
右键 → Create → C# Script
命名为"TankController"
```

### 2.2 TankController脚本内容

```csharp
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    
    [Header("射击设置")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    
    private Rigidbody rb;
    private float nextFireTime = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // 降低重心，防止翻车
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
    }
    
    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
    
    void HandleRotation()
    {
        float rotateInput = Input.GetAxis("Horizontal");
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
    
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // 给子弹添加向前的力
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * 20f;
            }
        }
    }
}
```

## 第三步：创建子弹系统

### 3.1 创建子弹预制体

```
在场景中创建子弹对象：
1. 右键Hierarchy → 3D Object → Sphere
2. 名称："Bullet"
3. Scale: (0.2, 0.2, 0.2)
4. 添加组件：
   - Rigidbody (勾选Use Gravity = false)
   - Sphere Collider (勾选Is Trigger = true)
```

### 3.2 创建子弹材质

```
在Materials文件夹中：
- 名称："Bullet_Material"
- Albedo颜色：黄色 (1, 1, 0)
- 应用到Bullet对象
```

### 3.3 创建BulletController脚本

```
在Scripts/Weapons文件夹中：
右键 → Create → C# Script
命名为"BulletController"
```

### 3.4 BulletController脚本内容

```csharp
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("子弹设置")]
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;
    
    void Start()
    {
        // 设置子弹生命周期
        Destroy(gameObject, lifeTime);
        
        // 设置子弹速度
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 击中墙壁或障碍物
        if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            // 检查是否是可破坏的砖墙
            if (other.name.Contains("BrickWall") || other.name.Contains("砖墙"))
            {
                Destroy(other.gameObject); // 摧毁砖墙
            }
            
            Destroy(gameObject); // 摧毁子弹
        }
        
        // 击中敌人
        if (other.CompareTag("Enemy"))
        {
            // 获取敌人的生命值组件
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            
            Destroy(gameObject); // 摧毁子弹
        }
    }
}
```

## 第四步：创建生命值系统

### 4.1 创建HealthSystem脚本

```
在Scripts/Managers文件夹中：
右键 → Create → C# Script
命名为"HealthSystem"
```

### 4.2 HealthSystem脚本内容

```csharp
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("生命值设置")]
    public int maxHealth = 3;
    public int currentHealth;
    
    [Header("效果设置")]
    public GameObject explosionEffect; // 爆炸特效预制体
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // 播放爆炸效果
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        
        // 如果是玩家，重新生成或游戏结束
        if (CompareTag("Player"))
        {
            // 这里可以调用游戏管理器的玩家死亡处理
            Debug.Log("玩家死亡！");
        }
        
        Destroy(gameObject);
    }
    
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
}
```

## 第五步：设置玩家坦克

### 5.1 为玩家坦克添加必要组件

```
选择PlayerTank对象：
1. 添加组件：
   - Rigidbody (Mass: 1, Drag: 2, Angular Drag: 5)
   - Box Collider (调整大小包围整个坦克)
   - TankController脚本
   - HealthSystem脚本

2. 设置标签：
   - Tag: Player
   - Layer: Player
```

### 5.2 配置TankController参数

```
在TankController组件中设置：
- Move Speed: 5
- Rotation Speed: 100
- Fire Rate: 0.5
- Fire Point: 将TankBarrel下的FirePoint拖拽到这里
- Bullet Prefab: 稍后创建预制体后拖拽
```

## 第六步：创建子弹预制体

### 6.1 设置子弹对象

```
为场景中的Bullet对象：
1. 添加BulletController脚本
2. 设置Tag为"Bullet"
3. 设置Layer为"Bullet"
4. 配置BulletController参数：
   - Speed: 20
   - Life Time: 5
   - Damage: 1
```

### 6.2 创建子弹预制体

```
将配置好的Bullet对象拖拽到Prefabs文件夹
然后从场景中删除Bullet对象
```

### 6.3 配置玩家坦克的子弹预制体

```
选择PlayerTank，在TankController组件中：
- 将Bullet预制体拖拽到Bullet Prefab字段
```

## 第七步：设置标签和碰撞

### 7.1 设置标签

```
Edit → Project Settings → Tags and Layers
创建标签：
- Player
- Enemy
- Bullet
- Wall
- Obstacle
- Base
```

### 7.2 为场景对象设置标签

```
- PlayerTank → Tag: Player
- 所有砖墙 → Tag: Wall
- 所有钢墙 → Tag: Wall  
- 边界墙 → Tag: Wall
- PlayerBase → Tag: Base
```

### 7.3 设置碰撞矩阵

```
Edit → Project Settings → Physics → Layer Collision Matrix
确保：
- Bullet层 与 Wall层 碰撞
- Bullet层 与 Player层 碰撞
- Bullet层 与 Enemy层 碰撞
- Player层 与 Wall层 碰撞
```

## 第八步：测试玩家控制

### 8.1 运行游戏测试

```
按Play按钮，测试以下功能：
- W/S键：前进/后退
- A/D键：左转/右转
- 空格键：射击
- 子弹击中砖墙：砖墙被摧毁
- 子弹击中钢墙：子弹消失，钢墙不受损
```

### 8.2 调整参数

```
根据测试结果调整：
- 移动速度是否合适
- 旋转速度是否流畅
- 射击频率是否合理
- 子弹速度是否合适
```

## 第九步：优化和完善

### 9.1 添加音效支持（可选）

```csharp
// 在TankController中添加
[Header("音效设置")]
public AudioClip shootSound;
public AudioClip moveSound;
private AudioSource audioSource;

void Start()
{
    audioSource = GetComponent<AudioSource>();
    // 其他初始化代码...
}

void Shoot()
{
    // 播放射击音效
    if (shootSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(shootSound);
    }
    
    // 其他射击代码...
}
```

## 检查清单

完成以上步骤后，你应该有：

- [x] 流畅的坦克移动控制（WASD）
- [x] 正常的坦克旋转
- [x] 空格键射击功能
- [x] 子弹飞行和碰撞检测
- [x] 砖墙可被摧毁
- [x] 钢墙无法被摧毁
- [x] 基础的生命值系统
- [x] 所有标签和层级设置正确

## 下一步预告

接下来我们将实现：

1. 敌人坦克AI系统
2. 敌人生成器
3. 游戏管理器
4. UI界面

现在可以测试玩家控制功能了！有什么问题随时告诉我。

# Unity 3D坦克大战 - 敌人AI与生成系统

## 第一步：创建敌人坦克预制体

### 1.1 创建敌人坦克模型

```
在场景中创建敌人坦克：
1. 右键Hierarchy → Create Empty
2. 名称："EnemyTank"
3. Position: (0, 0, 0)

创建敌人坦克身体：
- 右键EnemyTank → 3D Object → Cube
- 名称："EnemyTankBody"
- Position: (0, 0.5, 0)
- Scale: (2, 1, 3)

创建敌人坦克炮塔：
- 右键EnemyTank → 3D Object → Cube
- 名称："EnemyTankTurret"
- Position: (0, 1, 0)
- Scale: (1.5, 0.5, 1.5)

创建敌人坦克炮管：
- 右键EnemyTankTurret → 3D Object → Cube
- 名称："EnemyTankBarrel"
- Position: (0, 0, 1)
- Scale: (0.2, 0.2, 2)

创建敌人火力点：
- 右键EnemyTankBarrel → Create Empty
- 名称："EnemyFirePoint"
- Position: (0, 0, 1)
```

### 1.2 创建敌人坦克材质

```
在Materials文件夹中：
- 名称："EnemyTank_Material"
- Albedo颜色：红色 (0.8, 0.2, 0.2)
- 应用到EnemyTank的所有子对象
```

### 1.3 配置敌人坦克组件

```
选择EnemyTank对象，添加组件：
- NavMeshAgent (Speed: 3.5, Stopping Distance: 0.5)
- Rigidbody (Mass: 1, Drag: 2, Angular Drag: 5)
- Box Collider (调整大小包围整个坦克)
- HealthSystem脚本
- 设置Tag: Enemy
- 设置Layer: Enemy
```

## 第二步：创建敌人AI脚本

### 2.1 创建EnemyAI脚本

```
在Scripts/Enemy文件夹中：
右键 → Create → C# Script
命名为"EnemyAI"
```

### 2.2 EnemyAI脚本内容

```csharp
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("AI设置")]
    public float detectionRange = 10f;
    public float attackRange = 8f;
    public float patrolRadius = 5f;
    public float fireRate = 1f;
    
    [Header("移动设置")]
    public float rotationSpeed = 90f;
    
    [Header("武器设置")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    private NavMeshAgent agent;
    private Transform player;
    private float nextFireTime = 0f;
    private Vector3 startPosition;
    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private float patrolChangeInterval = 3f;
    
    public enum AIState
    {
        Patrol,
        Chase,
        Attack
    }
    
    public AIState currentState = AIState.Patrol;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPosition = transform.position;
        
        // 设置NavMeshAgent参数
        agent.speed = 3.5f;
        agent.stoppingDistance = 0.5f;
        agent.angularSpeed = rotationSpeed;
        
        SetRandomPatrolTarget();
    }
    
    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // 状态机
        switch (currentState)
        {
            case AIState.Patrol:
                PatrolBehavior();
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = AIState.Chase;
                }
                break;
                
            case AIState.Chase:
                ChaseBehavior();
                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attack;
                }
                else if (distanceToPlayer > detectionRange * 1.5f)
                {
                    currentState = AIState.Patrol;
                }
                break;
                
            case AIState.Attack:
                AttackBehavior();
                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Chase;
                }
                break;
        }
    }
    
    void PatrolBehavior()
    {
        // 巡逻行为
        if (agent.remainingDistance < 0.5f)
        {
            SetRandomPatrolTarget();
        }
        
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolChangeInterval)
        {
            SetRandomPatrolTarget();
            patrolTimer = 0f;
        }
    }
    
    void ChaseBehavior()
    {
        // 追击行为
        agent.SetDestination(player.position);
        LookAtPlayer();
    }
    
    void AttackBehavior()
    {
        // 攻击行为
        agent.SetDestination(transform.position); // 停止移动
        LookAtPlayer();
        
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void SetRandomPatrolTarget()
    {
        // 在起始位置周围随机选择巡逻点
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += startPosition;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            patrolTarget = hit.position;
            agent.SetDestination(patrolTarget);
        }
    }
    
    void LookAtPlayer()
    {
        // 让坦克面向玩家
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 保持水平
        
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed / 90f);
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // 创建敌人子弹
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // 设置子弹为敌人发射的
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet == null)
            {
                bullet.AddComponent<EnemyBullet>();
            }
            
            // 给子弹添加速度
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * 20f;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // 在Scene视图中显示检测范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, patrolRadius);
    }
}
```

## 第三步：创建敌人子弹系统

### 3.1 创建EnemyBullet脚本

```
在Scripts/Weapons文件夹中：
右键 → Create → C# Script
命名为"EnemyBullet"
```

### 3.2 EnemyBullet脚本内容

```csharp
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("敌人子弹设置")]
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
        
        // 设置子弹材质颜色为红色
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 击中墙壁或障碍物
        if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            // 检查是否是可破坏的砖墙
            if (other.name.Contains("BrickWall") || other.name.Contains("砖墙"))
            {
                Destroy(other.gameObject);
            }
            
            Destroy(gameObject);
        }
        
        // 击中玩家
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
        
        // 击中基地
        if (other.CompareTag("Base"))
        {
            // 游戏结束逻辑
            Debug.Log("基地被摧毁！游戏结束！");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
```

## 第四步：创建敌人生成器

### 4.1 创建EnemySpawner脚本

```
在Scripts/Enemy文件夹中：
右键 → Create → C# Script
命名为"EnemySpawner"
```

### 4.2 EnemySpawner脚本内容

```csharp
using UnityEngine;
using System.Collections.Generic;

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
    
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesSpawnedThisWave = 0;
    private float nextSpawnTime = 0f;
    private bool waveActive = true;
    
    void Start()
    {
        // 如果没有设置生成点，创建默认生成点
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            CreateDefaultSpawnPoints();
        }
        
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
        currentWave++;
        enemiesSpawnedThisWave = 0;
        waveActive = false;
        
        Debug.Log($"波次 {currentWave} 开始！");
        
        // 增加难度
        totalEnemiesPerWave += 2;
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
}
```

## 第五步：设置敌人坦克预制体

### 5.1 配置敌人坦克组件

```
选择EnemyTank对象：
1. 添加EnemyAI脚本
2. 配置EnemyAI参数：
   - Detection Range: 10
   - Attack Range: 8
   - Patrol Radius: 5
   - Fire Rate: 1
   - Rotation Speed: 90
   - Bullet Prefab: 将Bullet预制体拖拽到这里
   - Fire Point: 将EnemyFirePoint拖拽到这里

3. 配置HealthSystem参数：
   - Max Health: 1 (敌人坦克一击即死)
```

### 5.2 创建敌人坦克预制体

```
将配置好的EnemyTank拖拽到Prefabs文件夹
然后从场景中删除EnemyTank对象
```

## 第六步：创建敌人生成器对象

### 6.1 在场景中创建生成器

```
在场景中创建空对象：
1. 右键Hierarchy → Create Empty
2. 名称："EnemySpawner"
3. Position: (0, 0, 0)
4. 添加EnemySpawner脚本
```

### 6.2 配置生成器参数

```
在EnemySpawner组件中设置：
- Enemy Tank Prefab: 将EnemyTank预制体拖拽到这里
- Max Enemies: 5
- Spawn Interval: 3
- Total Enemies Per Wave: 10
- Current Wave: 1
- Wave Interval: 10
```

## 第七步：修改玩家子弹脚本

### 7.1 更新BulletController脚本

```csharp
// 在BulletController.cs的OnTriggerEnter方法中添加：
void OnTriggerEnter(Collider other)
{
    // 击中墙壁或障碍物
    if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
    {
        if (other.name.Contains("BrickWall") || other.name.Contains("砖墙"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    
    // 击中敌人 - 新增
    if (other.CompareTag("Enemy"))
    {
        HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        
        // 通知生成器敌人被摧毁
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnEnemyDestroyed();
        }
        
        Destroy(gameObject);
    }
}
```

## 第八步：测试敌人系统

### 8.1 运行游戏测试

```
按Play按钮，观察：
- 敌人坦克是否正常生成
- 敌人是否会巡逻
- 敌人是否会追击玩家
- 敌人是否会攻击玩家
- 玩家是否可以摧毁敌人
- 敌人子弹是否会伤害玩家
```

### 8.2 调试技巧

```
在Scene视图中选择敌人坦克，可以看到：
- 黄色球体：检测范围
- 红色球体：攻击范围
- 蓝色球体：巡逻范围
- 绿色线条：NavMesh路径
```

## 检查清单

完成以上步骤后，你应该有：

- [x] 红色的敌人坦克模型
- [x] 智能的敌人AI（巡逻、追击、攻击）
- [x] 敌人自动生成系统
- [x] 敌人可以射击红色子弹
- [x] 玩家可以摧毁敌人
- [x] 敌人可以伤害玩家
- [x] 波次系统（难度递增）
- [x] NavMesh寻路正常工作

## 下一步预告

接下来我们将实现：

1. 游戏管理器和UI系统
2. 得分系统
3. 游戏结束逻辑
4. 音效和特效

现在你已经有了一个基本可玩的坦克大战游戏！