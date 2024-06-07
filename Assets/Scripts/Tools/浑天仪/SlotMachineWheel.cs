using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SlotMachineWheel : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public float moveDistance = 30f;  // 以像素为单位的移动距离
    public float scrollDuration = 0.5f;  // 滚动动画持续时间，以秒为单位
    private ScrollRect scrollRect;
    private RectTransform content;
    public bool isAnimating = false;  // 控制动画状态的标志

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
    }

    public IEnumerator AnimateMove(float pixelDistance)
    {
        isAnimating = true;  // 开始动画，禁止其他输入

        // 计算移动的距离转换为normalized位置的比例
        float width = content.rect.width - ((RectTransform)scrollRect.transform).rect.width;
        float normalizedMove = pixelDistance / width;
        float targetNormalizedPosition = scrollRect.horizontalNormalizedPosition + normalizedMove;

        // 检查是否超出范围并调整目标位置
        if (targetNormalizedPosition < -0.1 || targetNormalizedPosition > 1.1)
        {
            targetNormalizedPosition = (pixelDistance < 0) ? 1f : 0f;  // 重置到对应的边界
        }

        float initialPosition = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0;

        // 动画过程
        while (elapsedTime < scrollDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scrollDuration;
            float smoothedT = Mathf.SmoothStep(0.0f, 1.0f, t);
            float newNormalizedPosition = Mathf.Lerp(initialPosition, targetNormalizedPosition, smoothedT);
            scrollRect.horizontalNormalizedPosition = newNormalizedPosition;
            yield return null;
        }

        // 确保最终位置精确
        scrollRect.horizontalNormalizedPosition = targetNormalizedPosition;
        isAnimating = false;  // 动画结束，允许新的输入

        // 查找所有Selectable实例
        Selectable[] selectables = FindObjectsOfType<Selectable>();
        foreach (var selectable in selectables)
        {
            selectable.isRotating = false;
        }
    }

    // 实现拖拽接口方法
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 始终阻止拖拽
        eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 始终阻止拖拽
        eventData.pointerDrag = null;
    }
}