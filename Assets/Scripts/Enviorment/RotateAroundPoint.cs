using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public Transform rotatePoint; // 旋转中心点
    public float rotationSpeed = 80f; // 旋转速度
    public Vector3 rotationAxis = Vector3.forward; // 旋转轴
    private void Update()
    {
        transform.RotateAround(rotatePoint.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
