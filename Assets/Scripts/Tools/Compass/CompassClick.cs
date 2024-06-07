using UnityEngine;

public class CompassClick : MonoBehaviour
{
    public Camera specificCamera; // 用于射线检测的特定相机
    public MonoBehaviour scriptToToggle; // 要切换启用/禁用的脚本
    public GameObject objectToDisable; // 初始状态被禁用的游戏对象
    public GameObject objectToEnable; // 初始状态被启用的游戏对象

    private bool isActive = true; // 跟踪当前状态

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Ray ray = specificCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // 确认是否点击了此脚本所在的游戏对象
                {
                    ToggleState();
                }
            }
        }
    }

    void ToggleState()
    {
        isActive = !isActive; // 切换状态

        if (scriptToToggle != null)
        {
            scriptToToggle.enabled = isActive;
        }

        if (isActive)
        {
            if (objectToEnable != null)
                objectToEnable.SetActive(true);
            if (objectToDisable != null)
                objectToDisable.SetActive(false);
        }
        else
        {
            if (objectToEnable != null)
                objectToEnable.SetActive(false);
            if (objectToDisable != null)
                objectToDisable.SetActive(true);
        }
    }
}
