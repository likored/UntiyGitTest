using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrack : EnemyController
{
    public float speed; //׷���ٶ�
    public float radius; //׷���뾶

    private Transform playerTransform; //���λ��
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        if(playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if(distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed*Time.deltaTime);
            }
        }
    }
}
