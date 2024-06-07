using UnityEngine;
using UnityEngine.UI;

public class PageTurner : MonoBehaviour
{
    public Image displayImage; // 确保在 Unity 编辑器中已经将某个 Image 组件赋值给此变量
    public Sprite[] pages;
    private int currentPageIndex = 0;

    void Start()
    {
        if (pages.Length > 0)
        {
            displayImage.sprite = pages[0]; // 在脚本加载时设置第一页
        }
        else
        {
            Debug.LogWarning("No pages available in the pages array.");
        }
    }

    public void TurnPageRight()
    {
        if (pages.Length == 0) return;

        currentPageIndex = (currentPageIndex + 1) % pages.Length;
        displayImage.sprite = pages[currentPageIndex];
    }

    public void TurnPageLeft()
    {
        if (pages.Length == 0) return;

        if (currentPageIndex == 0)
            currentPageIndex = pages.Length - 1;
        else
            currentPageIndex--;

        displayImage.sprite = pages[currentPageIndex];
    }

    public void ReplacePage(int index, Sprite newPage)
    {
        if (index >= 0 && index < pages.Length)
        {
            pages[index] = newPage;
            if (currentPageIndex == index)
            {
                displayImage.sprite = pages[index]; // Update image if current page is replaced
            }
        }
    }

    public void SetCurrentPage(int index)
    {
        if (index >= 0 && index < pages.Length)
        {
            currentPageIndex = index;
            displayImage.sprite = pages[currentPageIndex];
        }
    }
}