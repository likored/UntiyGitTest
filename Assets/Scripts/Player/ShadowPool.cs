using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    public GameObject shadowPrefab;

    public int shadowCount=10;

    private Queue<GameObject> availaObjects = new Queue<GameObject>(0);
    private void Awake()
    {
        instance = this;
        
         //初始化对象池
         FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i <shadowCount ; i++)
        {
            GameObject newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            
            //取消启用,返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);
        availaObjects.Enqueue(obj);
    }

    public GameObject getFormPool()
    {
        if (availaObjects.Count == 0)
        {
            FillPool();
        }
        
        GameObject outShadow = availaObjects.Dequeue();
        
        outShadow.SetActive(true);
        
        return outShadow;
    }
    
    
}
