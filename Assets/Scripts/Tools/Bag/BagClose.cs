using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagClose : MonoBehaviour
{
    public GameObject UI;

    // 切换 UI 和自由观察摄像机的可见性
    public void ToggleUIVisibility()
    {
        if (UI != null)
        {
            UI.SetActive(!UI.activeSelf);
        }
    }
}
