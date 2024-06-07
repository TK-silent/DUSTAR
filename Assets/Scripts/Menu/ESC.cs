using UnityEngine;
using Cinemachine;

public class ESC : MonoBehaviour, IOpenable
{
    public GameObject canvas;
    public GameObject startMenu;
    public GameObject globalSound;
    public Animator animator; // Animator 组件的引用
    public CinemachineFreeLook freeLookCamera;
    public ThirdPersonController playerMovement; // 引用 PlayerMovement 脚本

    public AudioSource audioSource;

    private bool isOpen = false;

    public bool IsOpen
    {
        get { return isOpen; }
    }
    
    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the canvas GameObject!");
        }

         if (freeLookCamera == null)
        {
            Debug.LogError("FreeLook Camera is not assigned!");
        }

        SetOpen(false); // 初始化UI为关闭状态
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                UIManager.Instance.RequestOpen(this);  // 请求打开ESC UI
            }
            else
            {
                UIManager.Instance.RequestClose(this);  // 请求关闭ESC UI
            }
        }
    }

    public void Back()
    {
        UIManager.Instance.RequestClose(this);
    }

    public void Quit()
    {
        UIManager.Instance.RequestClose(this);
        startMenu.SetActive(true);
        globalSound.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 实现 IOpenable 接口的 SetOpen 方法
    public void SetOpen(bool open)
    {
        if (isOpen != open)
        {
            isOpen = open;
            canvas.SetActive(true);
            freeLookCamera.enabled = !open;

            if (open)
            {
                animator.SetTrigger("TriggerEnter");
                playerMovement.UnableMovement();
            }
            else
            {
                animator.SetTrigger("TriggerExit");
                playerMovement.EnableMovement();
            }

            if (isOpen)
            {
                PauseAllAudio();
                audioSource.Play();
            }
            else
            {
                ResumeAllAudio();
            }
        }
    }

     // 暂停所有音频
    private void PauseAllAudio()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource src in sources)
        {
            if (src != audioSource)  // 避免暂停当前的 audioSource
                src.Pause();
        }

        globalSound.SetActive(false);
    }

    // 恢复所有音频
    private void ResumeAllAudio()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource src in sources)
        {
            if (src != audioSource)
                src.UnPause();
        }

        globalSound.SetActive(true);
    }

    public void DisableCanvas()
    {
        canvas.SetActive(false);
    }
}