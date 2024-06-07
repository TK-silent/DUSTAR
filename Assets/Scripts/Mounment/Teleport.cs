using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject objectToActivate; // 要激活的游戏对象
    public GameObject eUI;

    private bool isPlayerNear = false; // 玩家是否接近的状态
    private bool wasPlayerNear = false; // 上一帧玩家是否接近的状态，用于比较
    private bool isUIActive = false; // UI当前是否激活
    
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isUIActive)
            {
                TeleportToObject();
            }
            else
            {
                eUI.SetActive(false);
            }

        }

        if (isPlayerNear != wasPlayerNear)
        {
            eUI.SetActive(isPlayerNear);
            wasPlayerNear = isPlayerNear; // 更新上一状态
        }
    }

    private void TeleportToObject()
    {
        isUIActive = !isUIActive; // 切换UI激活状态

        objectToActivate.SetActive(true);

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