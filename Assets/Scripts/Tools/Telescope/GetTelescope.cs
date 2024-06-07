using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTelescope : MonoBehaviour
{
    public GameObject uiObject1; // 第一个UI对象
    public GameObject uiObject2; // 第二个UI对象
    public GameObject eUI;
    public GameObject getobject;
    public GameObject bagobject;
    public Animator uiAnimator;  // 控制UI动画的Animator组件
    public MissionPoint missionPoint;
    [TextArea(10,10)]
    public string tooltipText = "This is an item.";

    private bool isPlayerNear = false; // 玩家是否接近的状态
    private bool wasPlayerNear = false; // 上一帧玩家是否接近的状态，用于比较
    private bool isUIActive = false; // UI当前是否激活
    
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isUIActive)
            {
                OpenUI();
            }
            else
            {
                uiAnimator.SetTrigger("Empty");
                uiObject1.SetActive(false);
                uiObject2.SetActive(false);
                getobject.SetActive(false);
                bagobject.SetActive(true);
                eUI.SetActive(false);
                TooltipManager.Instance.ShowTooltip(tooltipText);
            }
        }

        if (isPlayerNear != wasPlayerNear)
        {
            eUI.SetActive(isPlayerNear);
            wasPlayerNear = isPlayerNear; // 更新上一状态
        }
    }

    private void OpenUI()
    {
        isUIActive = !isUIActive; // 切换UI激活状态

        uiObject1.SetActive(isUIActive);
        uiObject2.SetActive(isUIActive);

        // 控制动画的播放或还原
        if (uiAnimator != null)
        {
            if (isUIActive)
            {
                uiAnimator.SetTrigger("GetTelescope");
            }
            else
            {
                // 如果还原也需要动画，可以使用另一个触发器
                // 例如：uiAnimator.SetTrigger("HideCompass");
            }
        }

        missionPoint.TriggerNextTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerNear) // 确保是玩家触发的，且状态为未接近
        {
            isPlayerNear = true; // 只在玩家进入时设置一次
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerNear) // 确保是玩家触发的，且状态为接近
        {
            isPlayerNear = false; // 只在玩家离开时设置一次
        }
    }
}
