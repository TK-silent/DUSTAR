using UnityEngine;

public class DragCard : MonoBehaviour
{
    public Camera raycastCamera;  // 允许在编辑器中指定射线检测使用的相机
    public GameObject penObject;
    private bool isDragging = false;  // 标记是否正在拖拽对象
    private Vector3 screenPoint;
    private Vector3 offset;
    public float rotationSpeed = 100f;  // 旋转速度，可以在编辑器中调整

    void Update()
    {
        if (isDragging)
        {
            // 将鼠标位置转换为这台特定相机的世界坐标，但保持z坐标不变
            Vector3 mousePosition = raycastCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            // 更新对象的位置，只修改x和y坐标
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);

            // 读取鼠标滚轮输入并围绕Z轴旋转对象
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Rotate(0, 0, scroll * rotationSpeed * Time.deltaTime);
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
                    // 切换拖拽状态
                    isDragging = !isDragging;

                    // 根据是否正在拖拽来隐藏或显示鼠标光标
                    Cursor.visible = !isDragging;

                    if (isDragging)
                    {
                        // 计算对象的屏幕点和偏移量
                        screenPoint = raycastCamera.WorldToScreenPoint(gameObject.transform.position);
                        offset = gameObject.transform.position - raycastCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                    }
                }
            }

            if (isDragging)
            {
                if (penObject != null)
                {
                    Collider collider = penObject.GetComponent<Collider>();
                    if (collider != null)
                        collider.enabled = false;
                }
            }
            else
            {
                if (penObject != null)
                {
                    Collider collider = penObject.GetComponent<Collider>();
                    if (collider != null)
                        collider.enabled = true;
                }
            }
        }
    }
}