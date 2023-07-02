using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 光球初始化相关
/// </summary>
public class LightBall : MonoBehaviour
{
   
    //光球CD计时器
    public bool canShoot;
    public float waitTime = 2f;
    void Start()
    {
        //开启协程，用于光球发射CD计时
        StartCoroutine(WaitForNextShot());
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&canShoot)
        {
            canShoot = false;
            GameObject lightBall = Resources.Load<GameObject>("lightBall");
            //设置lightball按下鼠标的出生的位置
            lightBall.transform.position = GameObject.Find("lightBallPos").transform.position;
            
            Instantiate(lightBall);
            
           
        }
        
    }
    
    //↓：光球CD相关
    IEnumerator WaitForNextShot()
    {
        while (true)
        {
            ChangeState();
            yield return new WaitForSeconds(waitTime);
        }
       
    }

    void ChangeState()
    {
       
        if(!canShoot ) canShoot = !canShoot;
       
    }
    
}
