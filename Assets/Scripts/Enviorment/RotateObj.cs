using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float rotationSpeed = 40f; // 旋转速度

    private void Update()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime)); // 每帧旋转
    }
}
