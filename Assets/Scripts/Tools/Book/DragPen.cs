using UnityEngine;
using System.Collections.Generic;
using System;

public class DragPen : MonoBehaviour
{
    public Camera raycastCamera;  // 允许在编辑器中指定射线检测使用的相机
    public Material lineMaterial;
    public Transform lineParent; // 线的父对象
    public GameObject cardObject;
    public GameObject objectWithDragCard;
     public ConnectionManager connectionManager; // 引用连接管理器
    
    private bool isDragging = false;  // 标记是否正在拖拽对象
    private Vector3 screenPoint;
    private Vector3 originalPosition;  // 用于记录对象的原始位置
    private Transform firstObject;  // 用来存储第一个被点击的对象
    private List<GameObject> lines = new List<GameObject>();
    private Dictionary<string, GameObject> existingConnections = new Dictionary<string, GameObject>();

    void Awake()
    {
        // 保存对象的原始位置
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            // 将鼠标位置转换为这台特定相机的世界坐标，但保持z坐标不变
            Vector3 mousePosition = raycastCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            
            // 更新对象的位置，应用偏移
            transform.position = mousePosition;

            // 检查是否按下了鼠标右键
            if (Input.GetMouseButtonDown(1))
            {
                // 若正在拖拽且按下了右键，将对象回到初始位置
                transform.position = originalPosition;
                isDragging = false;
                Cursor.visible = true;
                // 启用碰撞器
                GetComponent<Collider>().enabled = true;
                cardObject.GetComponent<Collider>().enabled = true;
                objectWithDragCard.GetComponent<DragCard>().enabled = true;
            }
        }

        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 创建一条从指定相机发射到鼠标位置的射线
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 检查是否点击了当前对象
                if (hit.transform == transform)
                {
                    if (!isDragging)
                    {
                        // 切换拖拽状态
                        isDragging = true;

                        // 禁用碰撞器
                        GetComponent<Collider>().enabled = false;

                        cardObject.GetComponent<Collider>().enabled = false;
                        objectWithDragCard.GetComponent<DragCard>().enabled = false;

                        // 隐藏鼠标光标
                        Cursor.visible = false;

                        // 计算对象的屏幕点，此时不需要偏移量
                        screenPoint = raycastCamera.WorldToScreenPoint(gameObject.transform.position);
                    }
                }
            }

            if (isDragging)
            {
                TrySetLine();
            }
        }

        // 检测是否按下了x键
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearAllConnections(); // 调用清除所有连接的方法
        }
    }

    private LineRenderer CreateLineRenderer()
    {
        GameObject lineObj = new GameObject("Line");
        if (lineParent != null)
        {
            lineObj.transform.SetParent(lineParent, false);
        }
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        // 配置 LineRenderer
        lr.material = lineMaterial; // 根据需要更改材质
        lr.widthMultiplier = 0.048f;
        lr.positionCount = 2;
        lr.startColor = Color.red; // 设置起始颜色
        lr.endColor = Color.red;   // 设置结束颜色

        // 设置 GameObject 的 Layer
        lineObj.layer = LayerMask.NameToLayer("3DUI");

        // 添加线到列表
        lines.Add(lineObj);

        return lr;
    }

    private void TrySetLine()
    {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Connectable"))
            {
                if (firstObject == null)
                {
                    // 第一次点击一个可连接对象
                    firstObject = hit.transform;
                }
                else if (hit.transform != firstObject)
                {
                    // 点击第二个不同的可连接对象
                    string connectionKey = GetConnectionKey(firstObject, hit.transform);

                    if (!existingConnections.ContainsKey(connectionKey))
                    {
                        LineRenderer newLineRenderer = CreateLineRenderer();
                        newLineRenderer.SetPosition(0, firstObject.position);
                        newLineRenderer.SetPosition(1, hit.transform.position);

                        // 将新线添加到字典中
                        existingConnections[connectionKey] = newLineRenderer.gameObject;

                        // 使用连接管理器添加连接
                        connectionManager.AddConnection(firstObject, hit.transform);
                        firstObject = null;  // 重置第一个对象
                    }
                }
            }
        }
    }

    private string GetConnectionKey(Transform a, Transform b)
    {
        // 为了保证键的唯一性且不依赖于对象顺序，我们对两个对象的名称进行排序
        string[] names = new string[] { a.name, b.name };
        Array.Sort(names);  // 确保顺序一致性
        return string.Join("_", names);  // 如 "Object1_Object2"
    }

    public void ClearAllConnections()
    {
        // 销毁所有的线条 GameObjects
        foreach (GameObject line in lines)
        {
            if (line != null) // 确保GameObject仍然存在
            {
                Destroy(line);
            }
        }
        lines.Clear(); // 清空线条列表

        // 清空已存在的连接记录
        existingConnections.Clear();

        // 调用 ConnectionManager 的清空方法
        connectionManager.ClearAllConnections();

        // 重置第一个对象的引用
        firstObject = null;

        // 如果有更多的状态需要重置，可以在这里添加代码
    }

    public void FinishConnect()
    {
        transform.position = originalPosition;
        isDragging = false;
        Cursor.visible = true;
        // 启用碰撞器
        GetComponent<Collider>().enabled = true;
        cardObject.GetComponent<Collider>().enabled = true;
        objectWithDragCard.GetComponent<DragCard>().enabled = true;
        ClearAllConnections();
    }
}