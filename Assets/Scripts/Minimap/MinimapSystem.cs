using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapSystem : MonoBehaviour
{
    public RawImage minimapImage; // 拖拽MinimapDisplay赋值
    public Transform player; // 拖拽玩家坦克赋值

    void Update()
    {
        // 小地图旋转角度与玩家Y轴旋转相反（抵消方向偏差）
        minimapImage.rectTransform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
    }
}