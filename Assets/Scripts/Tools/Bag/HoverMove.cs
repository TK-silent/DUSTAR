using UnityEngine;

public class HoverMove : MonoBehaviour
{
    public Camera eventCamera; // 用于事件检测的相机
    public float moveDistance = 1.0f; // 移动距离
    public float animationSpeed = 2.0f; // 动画速度
    public CanvasGroup uiElement; // UI元素的Canvas Group
    public float fadeInSpeed = 1.0f; // UI渐入速度

    private Vector3 originalPosition; // 原始位置
    private Vector3 targetPosition; // 目标位置
    private bool isHovering = false; // 是否正在悬停
    private float targetAlpha = 0f; // 目标透明度

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;
        if (uiElement != null)
        {
            uiElement.alpha = 0; // 初始UI为完全透明
        }
    }

    void Update()
    {
        // 使用Lerp平滑地移向目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * animationSpeed);
        
        // 渐变更新UI的透明度
        if (uiElement != null)
        {
            uiElement.alpha = Mathf.Lerp(uiElement.alpha, targetAlpha, Time.deltaTime * fadeInSpeed);
        }

        // 检测鼠标悬停
        CheckMouseHover();
    }

    void CheckMouseHover()
    {
        if (eventCamera == null)
            return;

        Ray ray = eventCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                if (!isHovering)
                {
                    OnMouseEnter();
                }
            }
            else if (isHovering)
            {
                OnMouseExit();
            }
        }
        else if (isHovering)
        {
            OnMouseExit();
        }
    }

    void OnMouseEnter()
    {
        targetPosition = originalPosition + new Vector3(0, moveDistance, 0);
        isHovering = true;
        targetAlpha = 1.0f; // 开始渐入
    }

    void OnMouseExit()
    {
        targetPosition = originalPosition;
        isHovering = false;
        targetAlpha = 0.0f; // 开始渐隐
    }
}