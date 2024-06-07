using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    public GameObject objectToActivate1;
    public GameObject objectToActivate2;

    public GameObject rune1;
    public GameObject rune2;
    public GameObject rune3;

    public GameObject eUI;
    public AudioSource touchAudio;
    public MissionPoint missionPoint;
    private bool isPlayerNear = false;
    private bool wasPlayerNear = false;

    void Update()
    {
        // 检查玩家是否在触发区内并且按下了E键
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // 激活两个对象
            if (objectToActivate1 != null)
                objectToActivate1.SetActive(true);
            if (objectToActivate2 != null)
                objectToActivate2.SetActive(true);

            rune1.SetActive(true);
            rune2.SetActive(true);
            rune3.SetActive(true);

            touchAudio.Play();

            // 禁用当前对象
            missionPoint.TriggerNextTarget();
            eUI.SetActive(false);
            gameObject.SetActive(false);
        }

        if (isPlayerNear != wasPlayerNear)
        {
            eUI.SetActive(isPlayerNear);
            wasPlayerNear = isPlayerNear; // 更新上一状态
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的是否为玩家
        if (other.CompareTag("Player") && !isPlayerNear)
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 检查离开触发器的是否为玩家
        if (other.CompareTag("Player") && isPlayerNear)
        {
            isPlayerNear = false;
        }
    }
}