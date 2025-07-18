using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Clickable3DObject : MonoBehaviour, IPointerClickHandler
{
    public GameObject ClickPanel;
    void Start()
    {
    
        // 确保暂停面板初始状态为隐藏
        if (ClickPanel != null)
        {
            // 如果存在 ClickPanel ，那么就让他失活
            ClickPanel.SetActive(false);
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ClickPanel != null)
        {
            ClickPanel.SetActive(true);
        }
        Debug.Log("正在点击！");
        // 在这里添加点击后要执行的代码
    }
}

