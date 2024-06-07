using UnityEngine;
using Cinemachine;

public class Book : MonoBehaviour, IOpenable
{
    public Animator animator;  // 第一个动画器
    public GameObject UI;
    public CinemachineFreeLook freeLookCamera;
    public ThirdPersonController thirdPersonController;
    public GameObject zoomEffectObject;
    public Page pageScript;
    public GameObject specificObject; // 这是你想要检查是否激活的对象

    private ZoomEffect zoomEffectScript;
    private bool isOpen = false;  // 用来追踪UI是否已经打开
    public bool IsOpen
    {
        get { return isOpen; }
    }

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }
        if (thirdPersonController == null)
        {
            Debug.LogError("ThirdPersonController is not assigned.");
        }
        SetOpen(false);  // 初始化UI为关闭状态
        zoomEffectScript = zoomEffectObject.GetComponent<ZoomEffect>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !specificObject.activeSelf)
        {
            if (!isOpen)
            {
                UIManager.Instance.RequestOpen(this);  // 请求打开Book UI
            }
            else
            {
                UIManager.Instance.RequestClose(this);  // 请求关闭Book UI
            }
        }
    }

    public void UnLockPage()
    {
        if (!isOpen)
        {
            UIManager.Instance.RequestOpen(this);  // 请求打开Book UI
            pageScript.ShowDraftUI();
        }
    }

    public void CloseBook()
    {
        UIManager.Instance.RequestClose(this);
    }

    public void OpenBook()
    {
        UIManager.Instance.RequestOpen(this);
    }

    // 实现IOpenable接口的SetOpen方法
    public void SetOpen(bool open)
    {
        if (isOpen != open)
        {
            isOpen = open;

            UI.SetActive(true);
            freeLookCamera.enabled = !open;
            zoomEffectScript.enabled = !open;

            // 使用Trigger控制动画
            animator.SetTrigger(open ? "Open" : "Close");

            if (open)
            {
                thirdPersonController.UnableMovement();  // 禁止移动
            }
            else
            {
                thirdPersonController.EnableMovement();  // 允许移动
            }
        }

        TooltipManager.Instance.HideTooltip();
    }
}