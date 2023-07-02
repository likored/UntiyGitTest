using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health; //ÉúÃüÖµ
    public int damage; //ÉËº¦
    public float flashTime; //±äºìÉÁË¸Êµ¼ù

    private SpriteRenderer sr;
    private Color originalColor;
    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        //µĞÈËËÀÍö
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //ÊÜ»÷¼õÉúÃü£¬ÉÁË¸
    public void TakeDamage(int Damage)
    {
        health -= damage;
        FlashColor(flashTime);
        ResetColor();
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColoe", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }
}
