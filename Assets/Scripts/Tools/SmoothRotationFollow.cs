using UnityEngine;

public class SmoothRotationFollow : MonoBehaviour
{
    public float smoothTime = 3.5f; // 平滑过渡的时间
    public Vector3 minAngles = new Vector3(-30f, -30f, -30f); // 最小角度限制
    public Vector3 maxAngles = new Vector3(30f, 30f, 30f); // 最大角度限制

    private Quaternion targetRotation;

    void Start()
    {
        // 初始化目标旋转为当前旋转
        targetRotation = transform.localRotation;
    }

    void Update()
    {
        // 根据需要更新targetRotation
        // 例如：targetRotation = Quaternion.Euler(新的角度值);

        // 限制targetRotation的角度
        targetRotation = ClampRotation(targetRotation);

        // 平滑插值到目标旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime * Time.deltaTime);
    }

    Quaternion ClampRotation(Quaternion q)
    {
        Vector3 euler = q.eulerAngles;

        // 通过计算'360 + angle'和使用'%'运算符来处理超过0-360的情况
        euler.x = NormalizeAngle(euler.x);
        euler.y = NormalizeAngle(euler.y);
        euler.z = NormalizeAngle(euler.z);

        euler.x = Mathf.Clamp(euler.x, minAngles.x, maxAngles.x);
        euler.y = Mathf.Clamp(euler.y, minAngles.y, maxAngles.y);
        euler.z = Mathf.Clamp(euler.z, minAngles.z, maxAngles.z);

        return Quaternion.Euler(euler);
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }
}