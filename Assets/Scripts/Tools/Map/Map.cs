using UnityEngine;

public class Map : MonoBehaviour, IOpenable
{
    public GameObject mapUI;           // 指向UI面板的引用
    public GameObject freeLookCamera;  // 指向FreeLook Camera的引用
    private Animator mapAnimator;      // Animator组件的引用

    private bool isOpen = false;       // 内部状态追踪UI是否打开

    void Start()
    {
        // 获取Animator组件
        mapAnimator = mapUI.GetComponent<Animator>();
        if (mapAnimator == null)
        {
            Debug.LogError("Animator component is not attached to the map UI.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isOpen)
            {
                UIManager.Instance.RequestOpen(this);
            }
            else
            {
                UIManager.Instance.RequestClose(this);
            }
        }
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
        mapUI.SetActive(true);
        freeLookCamera.SetActive(!open);  // 当UI打开时禁用相机，反之亦然

        // 处理动画
        if (mapAnimator != null)
        {
            if (open)
            {
                mapAnimator.SetTrigger("Open");  // 触发打开动画
            }
            else
            {
                mapAnimator.SetTrigger("Close"); // 触发关闭动画
            }
        }
    }
}