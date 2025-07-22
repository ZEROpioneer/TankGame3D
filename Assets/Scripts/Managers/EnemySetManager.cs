using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetManager : MonoBehaviour
{
    [Header("当前面板组件")]
    // 手动指定当前面板的根对象（如果脚本挂载在子物体上）
    public GameObject currentPanel;
    [Header("UI控制")]
    // 自定义设置面板的 GameObject，需在 Inspector 面板拖拽赋值
    public GameObject customizeEnemySetPanel; 
    // 返回上一级的设置面板的 GameObject，需在 Inspector 面板拖拽赋值
    public GameObject setPanel;
    // 按钮
    public Button customizeButton;          // 返回上一页按钮
    public Button backButton;         // 自定义设置按钮
    private void Start()
    {
        // 添加点击监听

        customizeButton.onClick.AddListener(OnCustomizeButtonClick);
        
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    // “自定义设置”按钮点击事件处理
    private void OnCustomizeButtonClick()
    {
        // 隐藏当前敌人设置面板
        currentPanel.SetActive(false); 
        // 显示自定义设置面板
        customizeEnemySetPanel.SetActive(true); 
    }

    // “返回上一级”按钮点击事件处理
    private void OnBackButtonClick()
    {
        // 隐藏当前敌人设置面板
        currentPanel.SetActive(false); 
        // 显示上一级设置面板
        setPanel.SetActive(true); 
    }
}
