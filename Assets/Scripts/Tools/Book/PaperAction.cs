using UnityEngine;

public class PaperActions : MonoBehaviour
{
    public GameObject book;  // 直接引用Book对象作为GameObject
    public GameObject toggleButtons;
    public Items itemsScript;  // 引用Items脚本
    public ItemsImage itemsImageScript;  // 引用ItemsImage脚本
    public Animator animator;
    public GameObject paperUI;

    private void Update()
    {
        // 监听按键输入
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReactivateBookAndHideUI();
        }
    }

    // 调用Items脚本的ItemInteraction方法并禁用Book
    public void CallItemsInteraction()
    {
        if (itemsScript != null)
        {
            itemsScript.ItemInteraction();
            DisableBook();
        }
    }

    // 调用ItemsImage脚本的ItemInteraction方法并禁用Book
    public void CallItemsImageInteraction()
    {
        if (itemsImageScript != null)
        {
            itemsImageScript.ItemInteraction();
            DisableBook();
        }
    }

    // 禁用Book对象
    private void DisableBook()
    {
        if (book != null)
        {
            book.SetActive(false);
            toggleButtons.SetActive(false);
            paperUI.SetActive(false);
        }
    }

    // 按下E键时重新激活Book，并调用两个脚本的HideUI方法
    private void ReactivateBookAndHideUI()
    {
        if (book != null)
        {
            book.SetActive(true);
        }
        if (itemsScript != null)
        {
            itemsScript.HideUI();
        }
        if (itemsImageScript != null)
        {
            itemsImageScript.HideUI();
        }
        animator.SetTrigger("Open");
    }
}