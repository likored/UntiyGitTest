using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    public float fallSpeed = 10f; // 石头下落速度
    public float resetTime = 2f; // 石头重置时间
    private Vector3 initialPosition; // 石头初始位置
    private bool isFalling = false; // 石头是否正在下落
    private Vector3 randomXpos;
   

    private void Start()
    {
        initialPosition = transform.position; // 保存初始位置
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFalling) // 玩家触发陷阱
        {
            isFalling = true;
            Invoke("ResetTrap", resetTime); // 重置陷阱
        }
    }

    private void Update()
    {
        if (isFalling) // 石头正在下落
        {
            transform.position += Vector3.down * (fallSpeed * Time.deltaTime); // 石头下落
        }
    }

    private void ResetTrap()
    {
        randomXpos.x = Random.Range(-4, 4);
        isFalling = false;
        transform.position = initialPosition+randomXpos; // 重置石头位置
    }
}
