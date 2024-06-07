using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public LookAtObjectUI lookAtObjectUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 假设玩家的标签是"Player"
        {
            lookAtObjectUI.EnterTriggerArea(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lookAtObjectUI.ExitTriggerArea(gameObject);
        }
    }
}