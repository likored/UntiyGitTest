using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingStoneMachine : MonoBehaviour
{
    //下落位置判断相关
    private Transform startPos;
    public float fallDistance=10f;
    private Vector2 finalPos;
    private Vector2 beginPos;

    //状态判断相关
    private bool isFalling=false;
    private bool isRising = false;
    private bool isGrounded=false;
    
    //下落相关
    public float fallingSpeed = 10;
    public float risingSpeed = 5;
    
    //地面计时相关
    public float onGroundTime = 2f;
    private float groundTimer = 0f;

  

    void Start()
    {
        finalPos= new Vector2(transform.position.x, transform.position.y - fallDistance);
        beginPos = new Vector2(transform.position.x, transform.position.y);
    }

   
    void Update()
    {
        if(isFalling) Falling();
        if(!isFalling && isGrounded) StayOnGround();
        if(isRising) Rising();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!isRising)
        {
            
            isFalling = true;
            //机关下落
            
        }
    }


    private void Falling()
    {   
        transform.position = Vector3.MoveTowards(transform.position, finalPos, fallingSpeed * Time.deltaTime);
        //判断有没有下落到地面
        if (transform.position.y <= finalPos.y)
        {
            //下落到地面
            isFalling = false;
            isGrounded = true;
        }
    }

    private void StayOnGround()
    {
        groundTimer += Time.deltaTime;
        if (groundTimer >= onGroundTime)
        {
            //准备上升
            groundTimer = 0;
            isGrounded = false;
            isRising = true;
        }
    }

    private void Rising()
    {
        transform.position = Vector3.MoveTowards(transform.position, beginPos, risingSpeed * Time.deltaTime);
        if (transform.position.y == beginPos.y)
        {
            isRising = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y-fallDistance));
    }
    
    
}
