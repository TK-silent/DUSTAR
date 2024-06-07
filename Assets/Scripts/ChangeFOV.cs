using UnityEngine;
using Cinemachine;

public class ChangeFOV : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // 拖动你的FreeLook Camera到这里
    public float targetFOV = 60f; // 目标FOV
    public float changeRate = 0.5f; // FOV变化速率

    private float originalFOV; // 原始FOV
    private bool isInside = false; // 用于检查玩家是否在触发区内

    private void Start()
    {
        if (freeLookCamera != null)
        {
            originalFOV = freeLookCamera.m_Lens.FieldOfView; // 保存原始FOV
        }
    }

    private void Update()
    {
        if (isInside && freeLookCamera != null)
        {
            // 平滑地改变FOV到目标值
            freeLookCamera.m_Lens.FieldOfView = Mathf.Lerp(freeLookCamera.m_Lens.FieldOfView, targetFOV, changeRate * Time.deltaTime);
        }
        else if (!isInside && freeLookCamera != null)
        {
            // 平滑地恢复原始FOV
            freeLookCamera.m_Lens.FieldOfView = Mathf.Lerp(freeLookCamera.m_Lens.FieldOfView, originalFOV, changeRate * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}