using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bag : MonoBehaviour, IOpenable
{
    public GameObject backpackUI; // 背包UI的GameObject
    public GameObject bagUI;
    public Animator backpackAnimator; // Animator组件
    public CinemachineFreeLook freeLookCamera;
    public Camera itemCamera; // 用于渲染3D模型的专用相机
    public ThirdPersonController thirdPersonController;
    private bool isOpen = false;
    public bool IsOpen
    {
        get { return isOpen; }
    }

    public GameObject[] buttons; // 3D模型按钮数组
    public GameObject[] items; // 对应的3D模型对象数组
    private Dictionary<GameObject, GameObject> buttonToItemMap = new Dictionary<GameObject, GameObject>();
    private Camera mainCamera;

    void Start()
    {
        SetOpen(false);
        mainCamera = Camera.main; // 获取主摄像机
        SetupButtonToItemMap();
    }

    void SetupButtonToItemMap()
    {
        if (buttons.Length != items.Length)
        {
            Debug.LogError("Buttons and Items arrays must be of the same length!");
            return;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttonToItemMap[buttons[i]] = items[i];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
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

        if (isOpen && Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Ray ray = itemCamera.ScreenPointToRay(Input.mousePosition); // 使用专用相机创建射线
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (buttonToItemMap.ContainsKey(hit.collider.gameObject))
                {
                    ActivateItem(hit.collider.gameObject);
                }
            }
        }
    }

    public void SetOpen(bool open)
    {
        if (isOpen != open)
        {
            isOpen = open;
            backpackUI.SetActive(open);
            bagUI.SetActive(open);
            freeLookCamera.enabled = !open;
            backpackAnimator.SetTrigger(open ? "Open" : "Close");

            if (open)
                thirdPersonController.UnableMovement();
            else
                thirdPersonController.EnableMovement();
        }
    }

    private void ActivateItem(GameObject button)
    {
        GameObject itemToActivate;
        if (buttonToItemMap.TryGetValue(button, out itemToActivate))
        {
            foreach (var item in items) // 首先禁用所有模型
            {
                item.SetActive(false);
            }
            itemToActivate.SetActive(true); // 然后激活对应的模型
        }
    }
}