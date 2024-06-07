using UnityEngine;

public class TransferPlayer : MonoBehaviour
{
    public Transform player; // 玩家对象的 Transform
    public Transform target; // 目标对象的 Transform
    public GameObject UI;

    public void TeleportPlayerToTarget()
    {
        if (player != null && target != null)
        {
            player.position = target.position; // 将玩家的位置设置为目标的位置
        }
        else
        {
            Debug.LogError("Player or Target transform is not set.");
        }
    }

    public void CloseTransferUI()
    {
        UI.SetActive(false);
    }
}