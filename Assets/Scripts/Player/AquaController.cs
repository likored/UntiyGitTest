using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 玩家移动相关
/// </summary>
public class AquaController : MonoBehaviour
{
    private  Rigidbody2D rb;
    private Animator anim;
    
    private float moveInputDirection;
    public float moveSpeed=7;
    
    public Vector2 dir;
    private float horizontal;
    private float vertical;

    private bool isFacingRight=true;
    private bool canFlip;
    private bool isZipping=false;

    private bool isSwimming;
    private bool isZiped=false;
    
    // //------------------冲刺-------------------
    public float dashTime=0.15f;
    private float dashTimeLeft;
    private float lastDash=-10;
    public float dashSpeed=25;
    public float dashCoolDown = 1.5f;
    private bool isDashing;
    private bool canDash=true;

   // //------------------压缩-------------------
    public float zipCD = 5f;
    private float zipTimer = 0f;
    private bool isStartZip;

    //-------------------玩家受污染的数值-------------------------
    public float maxPolValue = 30f;
    private float currentPol;
    public float polMul = 0f;

  

  
    
    
            
   
    void Start()
    {
      
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentPol = 0;

    }

   
    void Update()
    {
      
        CheckIfDead();
        CheckInput();
        CheckMoveDirection();
        UpdateAnimations();
        UpdatePolValueAndZipTimer();
    }

    private void UpdatePolValueAndZipTimer()
    {
        currentPol += Time.deltaTime * polMul;

        if (isStartZip)
        {
            zipTimer += Time.deltaTime;
            if (zipTimer >= zipCD)
            {
                zipTimer = 0;
                isStartZip = false;
            }
        }
        

    }

    
    private void FixedUpdate()
    {
        Applymovement();
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isSwimming",isSwimming);
        anim.SetBool("isZipping",isZipping);

    }

    private void CheckInput()
    {
        moveInputDirection = Input.GetAxisRaw("Horizontal");
        
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //解决对角线方向移速过快问题
        if ((horizontal+ vertical) > 1|| (horizontal + vertical) < -1|| (horizontal >0.1&& vertical <-0.1)|| (horizontal < -0.1 && vertical> 0.1)) 
        { 
            horizontal = horizontal*(float)System.Math.Sqrt(0.5);
            vertical = vertical*(float)System.Math.Sqrt(0.5); 
        }
        
        dir = new Vector2(horizontal, vertical );

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= lastDash + dashCoolDown&&dir!=Vector2.zero)
            {
                //可以dash
                ReadyToDash();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(isStartZip) return;
            isZipping = !isZipping;
            anim.SetTrigger("isZiped");  //压缩与不压缩的状态切换

            isStartZip = true;
        }

    }
    
    

    private void Applymovement()
    {
        if(canDash)Dash();
        if (isDashing) return;
        
        if (canFlip) Flip();
       

        rb.velocity = (dir == Vector2.zero) ? Vector2.zero : dir * moveSpeed;
    }

    private void CheckMoveDirection()
    {
        if (isFacingRight && moveInputDirection<0)
        {
            canFlip = true;
        }

        if (!isFacingRight && moveInputDirection > 0)
        {
            canFlip = true;
        }

        if (dir!=Vector2.zero)
        {
            isSwimming = true;
        }

        else
        {
            isSwimming = false;
        }
    }
    
    private void Flip()
    {
        canFlip = false;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0f);
    }

  
   

    private void ReadyToDash()
    {
        canDash = true;
        isDashing = true;
        
        dashTimeLeft = dashTime;

        lastDash = Time.time;
    }

    private void Dash()
    {  
        
        if (isDashing)
        {
            if (dashTime > 0)
            {
                rb.velocity = (dir == Vector2.zero) ? Vector2.zero : dir * dashSpeed;

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.getFormPool();
            }
            
            
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canDash = false;
            }
        }

    }
    //-------------------玩家受伤-------------------------
   
    
    //-------------------玩家死翘翘-------------------------
    public void CheckIfDead()
    {
        if (currentPol >= maxPolValue)
        {
            Dead();
        }
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
    
    
    
}
