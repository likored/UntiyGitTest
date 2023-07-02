using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 光球被初始化后的蓄力行为、攻击行为、攻击反馈效果相关
/// </summary>
public class LightBallController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator anim;
    
    private Vector2 bowPositon;
    private Vector2 mousePosition;
    private Vector2 direction;
    
    //判断光球是否在玩家手中
    private bool isHolding=true;
    
    //光球在手中时
   
    public float minPower = 10f;//光球发射的最小力量（短按）
    public float maxPower = 50f;//光球发射的最大力量（长按）
    public float maxChargeTime = 2f;//长按的最大时间
    private float chargeTime;//长按中的时间
    private bool isCharging;//是否正在长按鼠标左键
    
    //光球击退效果相关
    public float explosionForce = 100f; // 爆炸时的力的大小
    public float explosionRadius = 0.5f; // 爆炸的半径
    
   //光球伤害相关
   public float attackDamage = 10f;

   private Vector2 targetPos;
   public float distanceFromPlayer=2f;
    
    
   
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        UpdateAnim();
        
        if (Input.GetMouseButton(0) && isHolding)
        {
            //插值计算按住鼠标左键的时间chargeTime
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
            
            
            //设置lightball按住鼠标时候的位置
            transform.position = GameObject.Find("lightBallPos").transform.position;
            
            //得到鼠标与玩家位置这两个点的向量
            bowPositon = transform.position;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - bowPositon;
            //把这个向量赋值给光球的 transform.right
            transform.right = direction;
            
            
            // //↓如果采用这种方法，要修复穿墙问题和初始化位置的问题!!!!
             targetPos = (Vector2)transform.position + direction.normalized * distanceFromPlayer;
             transform.position = targetPos;
        }

        if (Input.GetMouseButtonUp(0)&& isHolding)
        {   
            //设置lightball抬起鼠标时候的位置（瞬间）
            transform.position = targetPos;
            
            //transform.position = GameObject.Find("lightBallPos").transform.position;
            
            Shoot();
            isHolding = false;
            
        }
    }

    private void Shoot()
    {
        //rb.velocity = transform.right * lunchForce;
        
        //根据chargeTime和力量最大最小值，计算发射光球的力度
        float power = Mathf.Lerp(minPower, maxPower, chargeTime / maxChargeTime);
        // 在这里添加发射箭的代码，使用 power 控制箭的速度
        rb.velocity = transform.right * power;
        //重置相关属性，待下一次长按鼠标左键
        chargeTime = 0f;
        isCharging = false;
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (!isHolding)//光球在玩家手中时，不要爆炸
        {
            // 在碰撞点产生一个圆形的力场
            Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            
           
            
            foreach (Collider2D collider in detectedColliders)
            {
                
                
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (rb.transform.position - transform.position).normalized;
                    if (rb.gameObject.CompareTag("Player"))
                    {
                        //rb.velocity = -transform.forward * 10;
                    }
                    else
                    {
                        rb.AddForce(direction * explosionForce,ForceMode2D.Force);
                    }
                    
                   
                }
            }
            // 销毁光球
            Destroy(gameObject);
        }
       
    }

    private void UpdateAnim()
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!isHolding) Destroy(gameObject);
    }
}
