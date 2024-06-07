using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public Transform player; // Player的Transform组件
    public float mapWidth = 100f; // 地图的宽度
    public float mapLength = 100f; // 地图的长度

    private Vector3 initialPosition; // 记录初始位置

    void Start()
    {
        if (player != null)
        {
            initialPosition = player.position;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 计算角色相对于初始位置的移动比例
            float moveRatioX = (player.position.x - initialPosition.x) / mapWidth;
            float moveRatioZ = (player.position.z - initialPosition.z) / mapLength;

            // 根据角色的移动比例计算星空的旋转角度
            float rotationAngleX = moveRatioZ * 180f; // z轴移动影响x轴旋转
            float rotationAngleZ = moveRatioX * 180f; // x轴移动影响z轴旋转

            // 设置星空的旋转，这里我们让星空的旋转在水平面上显得是相反方向
            transform.rotation = Quaternion.Euler(-rotationAngleX, 0f, rotationAngleZ);
        }
    }
}