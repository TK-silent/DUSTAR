using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 更新为使用TextMeshPro命名空间

public class Items : MonoBehaviour
{
    public GameObject eUI;  // 用于显示提示按E键的UI
    public GameObject paperUI;
    public GameObject paperText;
    public GameObject bookPaper;
    public TextMeshProUGUI displayText;  // UI中用于显示交互文本的TextMeshProUGUI组件
    [TextArea(10,10)]
    public string itemText;  // 每个对象特定的文本内容
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
        bookPaper.SetActive(true);
        displayText.text = itemText;  // 设置文本为此对象特定的文本
        paperUI.SetActive(true);  // 确保文本组件是激活的
        paperAnimator.SetTrigger("Text");
        pickAudio.Play();
    }

    public void HideUI()
    {
        isUIActive = false;
        paperUI.SetActive(false);  // 隐藏文本组件
        paperText.SetActive(false);
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