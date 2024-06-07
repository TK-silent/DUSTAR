using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class ArtifactCameraController : MonoBehaviour, IOpenable
{
    public CinemachineFreeLook freeLookCamera;
    public GameObject mainCamera;
    public GameObject UICamera;
    public GameObject ModelsCamera;
    public List<Selectable> selectables;
    public float moveDistance = 15.0f;
    public float sensitivity = 100f;
    public float minY = -60f;
    public float maxY = 60f;
    public float transitionDuration = 1.0f;
    public float smoothFactor = 10f;
    public ThirdPersonController movementController;
    public GameObject Runes;
    public GameObject huntianyi; // 需要检查是否激活的对象

    private Vector3 originalPosition;
    private bool isMoved = false;
    private bool allowRotation = false;

    void Start()
    {
        if (mainCamera != null)
        {
            originalPosition = mainCamera.transform.position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && huntianyi.activeInHierarchy)
        {
            if (!isMoved)
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
        if (open && !isMoved)
        {
            MoveToArtifactView();
        }
        else if (!open && isMoved)
        {
            ReturnToOriginalView();
        }
    }

    private void MoveToArtifactView()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = false;
        }

        if (mainCamera != null)
        {
            Vector3 targetPosition = mainCamera.transform.position + new Vector3(0, moveDistance, 0);
            StopAllCoroutines();
            StartCoroutine(MoveCamera(targetPosition));
            StartCoroutine(RotateCameraToMaxY());
            isMoved = true;
            allowRotation = true;
        }

        if (movementController != null)
        {
            movementController.UnableMovement();
        }

        foreach (var selectable in selectables)
        {
            selectable.Deselect();
            if (selectable.collider != null)
            {
                selectable.collider.enabled = true;
            }
        }

        Runes.SetActive(true);
        UICamera.SetActive(false);
        ModelsCamera.SetActive(false);
    }

    private void ReturnToOriginalView()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = true;
        }

        if (mainCamera != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCamera(originalPosition));
            isMoved = false;
            allowRotation = false;
        }

        if (movementController != null)
        {
            movementController.EnableMovement();
        }

        foreach (var selectable in selectables)
        {
            selectable.InitializeSelectionState();
            if (selectable.collider != null)
            {
                selectable.collider.enabled = false;
            }
        }

        Runes.SetActive(false);
        UICamera.SetActive(true);
        ModelsCamera.SetActive(true);

        TooltipManager.Instance.HideTooltip();
    }

    IEnumerator MoveCamera(Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = mainCamera.transform.position;

        while (elapsedTime < transitionDuration)
        {
            float fraction = elapsedTime / transitionDuration;
            // 使用 SmoothStep 来调整 fraction 的值
            float smoothFraction = Mathf.SmoothStep(0.0f, 1.0f, fraction);
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, smoothFraction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // 确保精确到达目标位置
    }

    IEnumerator RotateCameraToMaxY()
    {
        float elapsedTime = 0;

        Quaternion startRotation = mainCamera.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(maxY, mainCamera.transform.eulerAngles.y, 0);

        while (elapsedTime < transitionDuration)
        {
            float fraction = elapsedTime / transitionDuration;
            // 使用 SmoothStep 来调整 fraction 的值
            float smoothFraction = Mathf.SmoothStep(0.0f, 1.0f, fraction);
            mainCamera.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, smoothFraction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localRotation = endRotation;  // 确保精确达到目标旋转
    }
}