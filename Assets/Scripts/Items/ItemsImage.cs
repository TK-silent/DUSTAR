using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 使用UnityEngine.UI命名空间以访问Image组件

public class ItemsImage : MonoBehaviour
{
    public GameObject eUI;  // 用于显示提示按E键的UI
    public GameObject paperUI;
    public GameObject bookPaper;
    public GameObject Image;
    public Image paperImage;  // UI中用于显示图片的Image组件
    public Sprite itemImage;  // 每个对象特定的图片
    public Animator paperAnimator;
    public AudioSource pickAudio;

    private bool isPlayerNear = false;
    private bool wasPlayerNear = false;
    private bool isUIActive = false;

    void Update()
    {
        // 检查是否在附近并按下E键
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isUIActive)
            {
                ItemInteraction();
            }
            else
            {
                HideUI();
            }
        }

        // 检查玩家是否接近状态有变化
        if (isPlayerNear != wasPlayerNear)
        {
            eUI.SetActive(isPlayerNear);  // 显示或隐藏提示UI
            wasPlayerNear = isPlayerNear;
        }
    }

    public void ItemInteraction()
    {
        isUIActive = true;
        paperUI.SetActive(true);
        bookPaper.SetActive(true);
        paperImage.sprite = itemImage;  // 为Image组件设置特定的图片
        paperImage.enabled = true;  // 确保图片可见
        paperAnimator.SetTrigger("Image");
        pickAudio.Play();
    }

    public void HideUI()
    {
        isUIActive = false;
        paperUI.SetActive(false);
        Image.SetActive(false);
        paperImage.enabled = false;  // 确保图片不可见
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerNear)
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerNear)
        {
            isPlayerNear = false;
            HideUI();  // 确保玩家离开时UI被隐藏
        }
    }
}