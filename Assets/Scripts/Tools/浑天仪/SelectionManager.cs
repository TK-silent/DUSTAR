using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    private List<Selectable> selectedObjects = new List<Selectable>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SelectObject(Selectable selectable)
    {
        // 如果点击的对象已经被选中并且是唯一被选中的对象，则不做任何操作
        if (selectedObjects.Count == 1 && selectedObjects.Contains(selectable))
        {
            return;
        }

        // 取消所有当前选中的对象
        DeselectAll();

        // 添加新的选中对象到列表并选中它
        selectedObjects.Add(selectable);
        selectable.Select();
    }

    public void DeselectAll()
    {
        foreach (var obj in selectedObjects)
        {
            obj.Deselect();
        }
        selectedObjects.Clear();
    }
}