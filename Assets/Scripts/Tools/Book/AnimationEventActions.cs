using UnityEngine;

public class AnimationEventActions : MonoBehaviour
{
    public Book book;

    // 动画事件调用此方法
    public void CompleteAnimationActions()
    {
        book.OpenBook();
        
        // 更新页面状态为 PageUI
        Page.lastActivePageState = Page.PageState.PageUI;
    }
}