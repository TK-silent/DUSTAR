using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController P_controller;
    public Transform N_camera;
    public Transform groundCheck; // 地面检测的Transform组件
    public LayerMask groundMask; // 地面层掩码

    public float walkSpeed = 3f; // 角色走路速度
    public float runSpeed = 6f; // 角色跑步速度
    public float gravity = -9.81f; // 重力加速度
    public float groundDistance = 1.0f; // 地面检测距离

    public float N_trunSmoothTime = 0.1f; // 角色转身缓冲时间
    float N_trunSmoothVelocity;

    Vector3 velocity; // 移动速度，包含重力
    bool isGrounded; // 角色是否站在地面上
    private bool isSitting = false;

    private void Update()
    {
        // 地面检测
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // 当角色在地面上时，如果速度向下，重置为一个很小的负值
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float N_horizontal = Input.GetAxisRaw("Horizontal");
        float N_vertical = Input.GetAxisRaw("Vertical");
        Vector3 P_direction = new Vector3(N_horizontal, 0, N_vertical).normalized;

        if (P_direction.magnitude >= 0.1f && !isSitting)
        {
            // 获取目标角度
            float N_targetAngle = Mathf.Atan2(P_direction.x, P_direction.z) * Mathf.Rad2Deg + N_camera.eulerAngles.y;
            // 平滑转动到目标角度
            float N_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, N_targetAngle, ref N_trunSmoothVelocity, N_trunSmoothTime);
            transform.rotation = Quaternion.Euler(0f, N_angle, 0f);

            // 判断是否按下Shift键来选择速度
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            // 计算移动方向
            Vector3 N_moveDir = Quaternion.Euler(0f, N_targetAngle, 0f) * Vector3.forward;
            P_controller.Move(N_moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        P_controller.Move(velocity * Time.deltaTime);
    }

    public void UnableMovement()
    {
        isSitting = true;
    }

    public void EnableMovement()
    {
        isSitting = false;
    }
}