using UnityEngine;

public class MissionPoint : MonoBehaviour
{
    public CompassPointer compassPointer; // 引用罗盘指针脚本
    public bool requiresExtraCondition = false; // 是否需要额外条件才能切换任务点，默认为 false

    private void OnTriggerEnter(Collider other)
    {
        // 检查触发器的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            if (!requiresExtraCondition)
            {
                // 如果不需要额外条件，直接切换到下一个任务点
                compassPointer.NextTarget();
            }
            else
            {
                // 如果需要额外条件，不立即切换任务点
                Debug.Log("已到达任务点，等待额外条件满足。");
            }
        }
    }

    // 可以从外部调用此方法来触发任务点的切换
    public void TriggerNextTarget()
    {
        if (requiresExtraCondition)
        {
            // 当额外条件被满足时，切换到下一个任务点
            compassPointer.NextTarget();
            Debug.Log("额外条件已满足，切换到下一个任务点。");
        }
    }
}