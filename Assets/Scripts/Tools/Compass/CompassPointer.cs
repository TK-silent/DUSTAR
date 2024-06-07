using UnityEngine;

public class CompassPointer : MonoBehaviour
{
    public enum Mode
    {
        TaskMode,
        NorthMode
    }

    public Transform finalTarget; // 北方最终目标对象
    public Transform[] targets; // 任务点的数组
    private int currentTargetIndex = 0; // 当前任务点的索引
    private Mode currentMode = Mode.TaskMode; // 当前模式，默认为任务模式

    void Update()
    {
        Transform target = null;

        if (currentMode == Mode.TaskMode && targets.Length > 0 && currentTargetIndex < targets.Length)
        {
            target = targets[currentTargetIndex];
        }
        else if (currentMode == Mode.NorthMode)
        {
            target = finalTarget;
        }

        if (target != null)
        {
            // 计算指向目标的方向向量
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // 忽略高度差，使指针平行于地面

            // 计算旋转角度
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360 * Time.deltaTime);

            // 获取当前物体的本地欧拉角
            Vector3 currentLocalEulerAngles = transform.localEulerAngles;

            // 设置X和Z轴的本地旋转为0度，保留Y轴上的本地旋转
            transform.localEulerAngles = new Vector3(0, currentLocalEulerAngles.y, 0);
        }
    }

    // 设置为北方模式
    public void SetNorthMode()
    {
        currentMode = Mode.NorthMode;
        Debug.Log("切换到北方模式，指向北方目标");
    }

    // 设置为任务模式
    public void SetTaskMode()
    {
        currentMode = Mode.TaskMode;
        TooltipManager.Instance.HideTooltip();
        Debug.Log("切换到任务模式，继续任务进度");
    }

    public void NextTarget()
    {
        if (currentMode == Mode.TaskMode)
        {
            if (currentTargetIndex < targets.Length - 1)
            {
                // 禁用当前任务点
                targets[currentTargetIndex].gameObject.SetActive(false);

                // 移动到下一个任务点
                currentTargetIndex++;

                // 激活新的当前任务点
                targets[currentTargetIndex].gameObject.SetActive(true);

                Debug.Log("切换到下一个任务点：" + targets[currentTargetIndex].name);
            }
            else if (currentTargetIndex == targets.Length - 1)
            {
                // 已到达任务列表的末尾，将最后一个任务点设置为北方最终目标对象
                // 禁用当前任务点
                targets[currentTargetIndex].gameObject.SetActive(false);

                // 将北方最终目标设置为当前任务点
                targets[currentTargetIndex] = finalTarget;

                // 激活最终目标任务点
                targets[currentTargetIndex].gameObject.SetActive(true);

                Debug.Log("所有任务点完成，最后一个任务点已切换至北方最终目标：" + finalTarget.name);
            }
        }
    }
}