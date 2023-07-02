using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    
    public float forceMagnitude = 10f; // 施加的力的大小
    private Vector2 forceDir;
    public AquaController player;


   

    private void OnTriggerEnter2D(Collider2D col)
    {
        forceDir = player.dir;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        
        if (other.gameObject.CompareTag("Player"))
        {
            print("233");
            Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
            
            rb.AddForce(forceDir*forceMagnitude,ForceMode2D.Force);
        }
    }
    
    
     private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(forceDir*forceMagnitude,ForceMode2D.Impulse);
            }
        }
    }
}
