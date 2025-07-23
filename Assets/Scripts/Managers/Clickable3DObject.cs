using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable3DObject : MonoBehaviour, IPointerClickHandler
{
    public GameObject ClickPanel;
    [Tooltip("双击间隔（秒），建议0.3-0.5")]
    public float doubleClickThreshold = 0.3f; // 双击判断的最大时间间隔
    private float lastClickTime; // 记录上一次点击的时间
    private int clickCount; // 记录点击次数（辅助判断）

    void Start()
    {
        // 3D对象必须有碰撞体才能被射线检测到（提示未添加的情况）
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("3D对象缺少Collider组件，点击无法生效！", this);
        }

        // 检查ClickPanel是否赋值
        if (ClickPanel == null)
        {
            Debug.LogWarning("请在Inspector中给ClickPanel赋值！", this);
        }
        else
        {
            ClickPanel.SetActive(false); // 初始隐藏面板
        }
    }

    // 3D对象被点击时触发（由Physics Raycaster检测）
    public void OnPointerClick(PointerEventData eventData)
    {
        // 只响应左键点击（3D对象通常用左键交互，可选）
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        float currentTime = Time.time;
        clickCount++;

        // 如果两次点击间隔在阈值内，判定为双击
        if (currentTime - lastClickTime <= doubleClickThreshold && clickCount >= 2)
        {
            DoubleClickAction(); // 执行双击逻辑
            clickCount = 0; // 重置点击计数
        }
        else
        {
            // 超过间隔，重置计数（只保留最近一次点击）
            clickCount = 1;
        }

        // 更新上一次点击时间
        lastClickTime = currentTime;
    }

    // 双击3D对象后执行的操作
    private void DoubleClickAction()
    {
        if (ClickPanel != null)
        {
            ClickPanel.SetActive(!ClickPanel.activeSelf); // 切换面板显示状态
            Debug.Log("3D对象被双击！面板状态：" + ClickPanel.activeSelf);
        }
    }
}