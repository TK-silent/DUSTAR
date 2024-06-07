using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotationPeriod = 120f; // 旋转一周所需时间，以秒为单位
    public Vector3 rotationAxis = Vector3.up; // 旋转轴，默认为y轴
    public Transform rotationCenterTransform; // 旋转中心对象

    private float rotationSpeed; // 每秒旋转的角度

    void Start()
    {
        // 计算每秒旋转的角度
        rotationSpeed = 360f / rotationPeriod;
    }

    void Update()
    {
        if (rotationCenterTransform != null)
        {
            // 获取旋转中心的位置
            Vector3 rotationCenter = rotationCenterTransform.position;

            // 计算物体到旋转中心的向量
            Vector3 toCenter = transform.position - rotationCenter;

            // 创建旋转四元数
            Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis);

            // 更新物体到旋转中心的向量
            toCenter = rotation * toCenter;

            // 更新物体的位置
            transform.position = rotationCenter + toCenter;

            // 旋转物体以保持正确的朝向
            transform.rotation = rotation * transform.rotation;
        }
    }
}