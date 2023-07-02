using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;

    public float activeTime=0.3f;
    public float activeStart;

    private float alpha;
    public float alphaSet=1f;
    public float alphaMul=0.6f;

    private void Start()
    {
      
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.transform.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    void FixedUpdate()
    {
        alpha *= alphaMul;
        color = new Color(0.4f, 0.8f, 1, alpha);
        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池  
            ShadowPool.instance.ReturnPool(gameObject);
        }
    }
}
