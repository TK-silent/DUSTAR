using UnityEngine;

public class HugeRune : MonoBehaviour
{
    public GameObject eUI;               // 用来显示和隐藏交互UI
    public GameObject objectToActivate;  // 要激活的对象
    public GameObject compass;
    public GameObject telescope;
    public Animator animator1;
    public Animator animator2;
    public GameObject[] objectsToChangeMaterial; // 需要更改材质的对象数组
    public Material newMaterial;         // 新材质
    public MissionPoint missionPoint;    // 任务点
    private bool isPlayerNear = false;
    private bool wasPlayerNear = false;

    void Update()
    {
        // 检查玩家是否在触发区内并且按下了E键
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            missionPoint.TriggerNextTarget();
            objectToActivate.SetActive(true); // 激活指定对象
            compass.SetActive(false);
            telescope.SetActive(false);
            animator1.SetTrigger("ON");

            // 更改每个指定对象的第一个材质，并如果存在第二个材质，将其设置为null
            foreach (GameObject obj in objectsToChangeMaterial)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && renderer.materials.Length > 0)
                {
                    Material[] materials = renderer.materials;

                    // 设置第一个材质为新材质
                    materials[0] = newMaterial;

                    // 如果存在第二个材质，将其设置为null
                    if (materials.Length > 1)
                    {
                        materials[1] = null;
                    }

                    renderer.materials = materials; // 应用新材质数组
                }
            }

            animator2.SetTrigger("Shining");

            eUI.SetActive(false);
            gameObject.SetActive(false); // 禁用脚本所在的对象
        }

        // 控制交互UI的显示与隐藏
        if (isPlayerNear != wasPlayerNear)
        {
            eUI.SetActive(isPlayerNear);
            wasPlayerNear = isPlayerNear; // 更新上一状态
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的是否为玩家
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 检查离开触发器的是否为玩家
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}