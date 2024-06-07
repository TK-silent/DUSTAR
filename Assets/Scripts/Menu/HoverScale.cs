using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.1f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    public float duration = 0.5f; // 动画持续时间

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * scaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();  // 停止所有协程以避免冲突
        StartCoroutine(ScaleTo(targetScale, duration));  // 开始放大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();  // 停止所有协程以避免冲突
        StartCoroutine(ScaleTo(originalScale, duration));  // 开始缩小
    }

    IEnumerator ScaleTo(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0.0f, 1.0f, t); // 使用 SmoothStep 来平滑插值
            transform.localScale = Vector3.Lerp(startScale, target, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;  // 确保最终缩放值精确
    }
}