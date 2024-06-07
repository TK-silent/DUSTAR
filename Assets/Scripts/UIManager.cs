using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {
        get {
            if (instance == null) {
                var obj = new GameObject("UIManager");
                instance = obj.AddComponent<UIManager>();
            }
            return instance;
        }
    }

    private IOpenable currentlyOpenUI;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        UpdateMouseLock();
    }

    public void RequestOpen(IOpenable ui) {
        if (currentlyOpenUI != null && currentlyOpenUI != ui) {
            currentlyOpenUI.SetOpen(false);
        }
        currentlyOpenUI = ui;
        ui.SetOpen(true);
        UpdateMouseLock();
    }

    public void RequestClose(IOpenable ui) {
        if (currentlyOpenUI == ui) {
            currentlyOpenUI = null;
            ui.SetOpen(false);
            UpdateMouseLock();
        }
    }

    // 更新鼠标的锁定状态和可见性
    private void UpdateMouseLock() {
        if (currentlyOpenUI == null) {
            // 没有UI激活时，锁定鼠标并隐藏
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            // 有UI激活时，解锁鼠标并显示
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}