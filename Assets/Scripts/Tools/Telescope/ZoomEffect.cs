using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class ZoomEffect : MonoBehaviour, IOpenable
{
    public GameObject ZoomUI;
    public GameObject Player;
    public GameObject AdditionalUI;
    private CanvasGroup additionalUICanvasGroup;
    public Material targetMaterial;
    public CinemachineFreeLook freeLookCamera;
    public GameObject wangyuanjing; 
    public float zoomFactor = 0.0f;
    public float maxZoomFactor = 1.0f;
    public float zoomSpeed = 0.1f;
    public float alternativeMaxZoomFactor = 2.0f;
    private bool usingAlternativeZoom = false;
    private Coroutine currentCoroutine;
    private float t = 0;
    private float originalRadius;
    private bool isZoom;

    [SerializeField]
	UniversalRendererData renderData;

    void Start()
    {
        InitializeUI();
        originalRadius = freeLookCamera.m_Orbits[2].m_Radius;
    }

    void Update()
    {
        HandleZoomInput();
    }

    private void InitializeUI()
    {
        zoomFactor = 0.0f;
        targetMaterial.SetFloat("_ZoomAmount", zoomFactor);
        additionalUICanvasGroup = AdditionalUI.GetComponent<CanvasGroup>();
        additionalUICanvasGroup.alpha = 0;
    }

    private void HandleZoomInput()
    {
        if (Input.GetMouseButtonDown(1) && wangyuanjing.activeInHierarchy)
        {
            UIManager.Instance.RequestOpen(this);
            StartZoom(usingAlternativeZoom ? alternativeMaxZoomFactor : maxZoomFactor);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            renderData.rendererFeatures[2].SetActive(false);
            TooltipManager.Instance.HideTooltip();
        }

        if (Input.GetMouseButtonUp(1))
        {
            UIManager.Instance.RequestClose(this);
            EndZoom();
        }

        if (Input.GetMouseButton(1) && Input.mouseScrollDelta.y != 0)
        {
            usingAlternativeZoom = Input.mouseScrollDelta.y > 0;
            StartZoom(usingAlternativeZoom ? alternativeMaxZoomFactor : maxZoomFactor);
        }
    }

    private void StartZoom(float targetZoom)
    {
        freeLookCamera.m_Orbits[2].m_Radius = 0.01f;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ChangeZoomAmount(targetZoom, true));
    }

    public void EndZoom()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ChangeZoomAmount(0.0f, false));
        freeLookCamera.m_Orbits[2].m_Radius = originalRadius;
        renderData.rendererFeatures[2].SetActive(true);
    }

    public void SetOpen(bool open)
    {
        ZoomUI.SetActive(open);
        Player.SetActive(!open);
        AdditionalUI.SetActive(open);
        if (!open) EndZoom();
    }

    IEnumerator ChangeZoomAmount(float targetZoom, bool increasing)
    {
        t = 0;
        float startZoom = zoomFactor;
        float startAlpha = additionalUICanvasGroup.alpha;
        float targetAlpha = increasing ? 1.0f : 0.0f;

        while (!Mathf.Approximately(zoomFactor, targetZoom) || !Mathf.Approximately(additionalUICanvasGroup.alpha, targetAlpha))
        {
            t += Time.deltaTime * zoomSpeed;
            zoomFactor = Mathf.SmoothStep(startZoom, targetZoom, t);
            additionalUICanvasGroup.alpha = Mathf.SmoothStep(startAlpha, targetAlpha, t);
            targetMaterial.SetFloat("_ZoomAmount", zoomFactor);
            yield return null;
        }

        if (targetZoom == 0.0f)
        {
            UIManager.Instance.RequestClose(this);
        }
    }
}