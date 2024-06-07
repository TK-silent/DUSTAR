using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialTextManager : MonoBehaviour
{
    public TMP_Text textMeshPro;
    [TextArea(10,10)]
    public string tooltipText = "This is an item.";
    public GameObject startCamera;
    [TextArea(3,10)]
    public string[] messages;
    public float fadeDuration = 2.0f;
    public float waitBeforeDisable = 5.0f;
    private int currentMessageIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentMessageIndex == 0)
        {
            StartCoroutine(ChangeText(1)); // 第一次按 W 切换到第二条消息
        }
        else if (Input.GetKeyDown(KeyCode.Z) && currentMessageIndex == 1)
        {
            StartCoroutine(ChangeText(2)); // 第一次按 Z 切换到第三条消息
        }
    }

    private IEnumerator ChangeText(int nextMessageIndex)
    {
        // 淡出
        yield return StartCoroutine(FadeTextTo(0.0f, fadeDuration));
        // 更改文本
        currentMessageIndex = nextMessageIndex;
        if (currentMessageIndex < messages.Length)
        {
            textMeshPro.text = messages[currentMessageIndex];
            // 淡入
            yield return StartCoroutine(FadeTextTo(1.0f, fadeDuration));
        }

        // 如果是最后一条消息
        if (currentMessageIndex == messages.Length - 1)
        {
            yield return new WaitForSeconds(waitBeforeDisable);
            // 淡出并禁用
            StartCoroutine(DisableText());
        }
    }

    private IEnumerator FadeTextTo(float targetAlpha, float duration)
    {
        Color currentColor = textMeshPro.color;
        float startAlpha = currentColor.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(startAlpha, targetAlpha, t));
            textMeshPro.color = newColor;
            yield return null;
        }

        textMeshPro.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

    private IEnumerator DisableText()
    {
        yield return StartCoroutine(FadeTextTo(0.0f, fadeDuration));
        TooltipManager.Instance.ShowTooltip(tooltipText);
        gameObject.SetActive(false);
        startCamera.SetActive(false);
    }
}