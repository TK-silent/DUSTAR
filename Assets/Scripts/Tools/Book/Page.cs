using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public enum PageState
    {
        PageUI,
        DraftUI,
        PaperUI,
        None  // 用于表示没有任何UI界面激活的状态
    }

    public GameObject pageUI;
    public GameObject draftUI;
    public GameObject penObject;
    public GameObject draftButtons;
    public GameObject toggleButtonUI;
    public GameObject paper;
    public GameObject paperButtonUI;

    // 使用枚举来跟踪最后激活的页面状态
    public static PageState lastActivePageState = PageState.PageUI;

    // 方法供动画事件调用
    public void InitializeUI()
    {
        switch (lastActivePageState)
        {
            case PageState.PageUI:
                ShowPageUI();
                break;
            case PageState.DraftUI:
                ShowDraftUI();
                break;
            case PageState.PaperUI:
                ShowPaperUI();
                break;
            default:
                CloseAllUI();
                break;
        }
    }

    public void ShowPageUI()
    {
        pageUI.SetActive(true);
        paper.SetActive(false);
        paperButtonUI.SetActive(false);
        draftUI.SetActive(false);
        penObject.SetActive(false);
        draftButtons.SetActive(false);
        toggleButtonUI.SetActive(true);
        lastActivePageState = PageState.PageUI;
    }

    public void ShowDraftUI()
    {
        pageUI.SetActive(false);
        paper.SetActive(false);
        paperButtonUI.SetActive(false);
        draftUI.SetActive(true);
        penObject.SetActive(true);
        draftButtons.SetActive(true);
        toggleButtonUI.SetActive(true);
        lastActivePageState = PageState.DraftUI;
    }

    public void ShowPaperUI()
    {
        pageUI.SetActive(false);
        draftUI.SetActive(false);
        penObject.SetActive(false);
        draftButtons.SetActive(false);
        paper.SetActive(true);
        paperButtonUI.SetActive(true);
        toggleButtonUI.SetActive(true);
        lastActivePageState = PageState.PaperUI;
    }

    // 关闭所有UI界面
    public void CloseAllUI()
    {
        pageUI.SetActive(false);
        draftUI.SetActive(false);
        penObject.SetActive(false);
        draftButtons.SetActive(false);
        toggleButtonUI.SetActive(false);
        paper.SetActive(false);
        paperButtonUI.SetActive(false);
    }
}