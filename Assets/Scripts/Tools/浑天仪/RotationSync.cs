using UnityEngine;

public class RotationSync : MonoBehaviour
{
    public Transform target;  // 目标物体的Transform组件

    void Update()
    {
        // 每帧更新当前物体的旋转，使其与目标物体的旋转相同
        transform.rotation = target.rotation;
    }
}