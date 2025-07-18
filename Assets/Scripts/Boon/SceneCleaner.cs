using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCleaner : MonoBehaviour
{
    public void DestroyAllSceneObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 防止销毁自己
            if (obj != this.gameObject && !obj.CompareTag("MainCamera"))
            {
                Destroy(obj);
            }
        }

        Debug.Log("所有对象已销毁。");
    }
}


