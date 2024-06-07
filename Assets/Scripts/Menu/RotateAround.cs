using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform centerObject; // 中心对象
    public float rotationSpeed = 30.0f; // 每秒旋转的角度
    public Transform[] objectsToRotate; // 需要旋转的对象数组

    void Update()
    {
        if (centerObject == null)
            return;

        foreach (Transform obj in objectsToRotate)
        {
            // 让每个对象围绕中心对象的Z轴旋转
            obj.RotateAround(centerObject.position, centerObject.forward, rotationSpeed * Time.deltaTime);
        }
    }
}