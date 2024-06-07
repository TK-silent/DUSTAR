using System.Collections.Generic;
using UnityEngine;

public class DraftTurner : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>(); // 使用 List 替代数组

    private int currentPageIndex = 0;

    void Start()
    {
        if (pages.Count > 0)
        {
            SetActivePage(currentPageIndex);
        }
        else
        {
            Debug.LogWarning("No pages available in the pages list.");
        }
    }

    private void SetActivePage(int index)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == index);
        }
    }

    public void TurnPageRight()
    {
        if (pages.Count == 0) return;

        currentPageIndex = (currentPageIndex + 1) % pages.Count;
        SetActivePage(currentPageIndex);
    }

    public void TurnPageLeft()
    {
        if (pages.Count == 0) return;

        if (currentPageIndex == 0)
            currentPageIndex = pages.Count - 1;
        else
            currentPageIndex--;

        SetActivePage(currentPageIndex);
    }

    public void AddPage(GameObject newPage)
    {
        if (!pages.Contains(newPage)) // 检查页面是否已经存在于列表中
        {
            pages.Add(newPage);
            SetActivePage(pages.Count - 1); // 激活新添加的页面
        }
        else
        {
            Debug.LogWarning("This page is already added.");
        }
    }

    public void SetCurrentPage(int index)
    {
        if (index >= 0 && index < pages.Count)
        {
            currentPageIndex = index;
            SetActivePage(currentPageIndex);
        }
    }
}