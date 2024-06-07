using UnityEngine;
using System.Collections.Generic;

public class LookAtObjectUI : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject uiElement;
    public Book book;
    public ZoomEffect zoomEffect;
    public DraftTurner draftTurner;
    public float maxDistance = 5.0f;
    private float mouseHoldTime = 0f;

    // 用 HashSet 来存储当前玩家所在的触发区域
    private HashSet<GameObject> currentTriggerAreas = new HashSet<GameObject>();

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance))
        {
            InteractiveObjectData interactiveData = hit.collider.GetComponent<InteractiveObjectData>();
            if (interactiveData != null && currentTriggerAreas.Contains(interactiveData.triggerArea))
            {
                ShowUI();

                if (Input.GetMouseButtonDown(0))
                {
                    mouseHoldTime = 0f;
                }

                if (Input.GetMouseButton(0))
                {
                    mouseHoldTime += Time.deltaTime;

                    if (mouseHoldTime >= 2.0f) // 长按时间
                    {
                        ProcessInteraction(interactiveData);
                        mouseHoldTime = 0f; // 重置计时器
                    }
                }
            }
            else
            {
                HideUI();
            }
        }
        else
        {
            HideUI();
        }
    }

    public void EnterTriggerArea(GameObject area)
    {
        currentTriggerAreas.Add(area);
    }

    public void ExitTriggerArea(GameObject area)
    {
        currentTriggerAreas.Remove(area);
    }

    private void ShowUI()
    {
        if (!uiElement.activeInHierarchy)
        {
            uiElement.SetActive(true);
        }
    }

    private void HideUI()
    {
        if (uiElement.activeInHierarchy)
        {
            uiElement.SetActive(false);
        }
    }

    private void ProcessInteraction(InteractiveObjectData data)
    {
        // 可以访问 data 中的任何信息来执行特定的逻辑
        draftTurner.AddPage(data.replacementObject);
        book.UnLockPage();
        zoomEffect.EndZoom();
    }
}