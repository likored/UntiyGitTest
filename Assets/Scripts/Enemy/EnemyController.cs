using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health; //����ֵ
    public int damage; //�˺�
    public float flashTime; //�����˸ʵ��

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
        //��������
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //�ܻ�����������˸
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
