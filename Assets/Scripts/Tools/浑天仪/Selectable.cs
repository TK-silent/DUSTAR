using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Material highlightMaterial;  // 高亮材质
    public Transform targetToRotate;    // 要旋转的目标物体
    public Transform secondTargetToRotate;    // 第二个要旋转的目标物体
    public Collider collider;
    private Renderer myRenderer;        // 渲染器组件
    private Quaternion targetRotation;  // 目标旋转
    private bool isSelected = false;    // 判断物体是否被选中
    private float rotationAngle = 45.0f;  // 旋转角度
    public float rotationSpeed = 0.1f;  // 旋转速度
    public bool isRotating = false;  // 用于跟踪是否正在旋转
    public SlotMachineWheel slotMachineWheel;  // 引用 SlotMachineWheel

    // 定义旋转轴向的枚举
    public enum RotationAxis
    {
        XAxis,
        YAxis,
        ZAxis
    }
    public RotationAxis rotationAxis = RotationAxis.YAxis;  // 默认旋转轴为Y轴

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        if (targetToRotate != null)
            targetRotation = targetToRotate.rotation;

        collider.enabled = false;

        InitializeSelectionState();
    }

    void Update()
    {
        if (isSelected && targetToRotate != null)
        {
            RotateObject();
            // 应用平滑旋转到第一个目标物体
            targetToRotate.rotation = Quaternion.Slerp(targetToRotate.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            // 如果第二个目标物体存在，则也应用平滑旋转
            if (secondTargetToRotate != null)
            {
                secondTargetToRotate.rotation = Quaternion.Slerp(secondTargetToRotate.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        // 如果未选中且第二个目标物体存在，则只对第二个目标物体应用平滑旋转
        if (!isSelected && secondTargetToRotate != null)
        {
            secondTargetToRotate.rotation = Quaternion.Slerp(secondTargetToRotate.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (isSelected && targetToRotate != null && !slotMachineWheel.isAnimating)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(slotMachineWheel.AnimateMove(-slotMachineWheel.moveDistance));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(slotMachineWheel.AnimateMove(slotMachineWheel.moveDistance));
            }
        }
    }

    private void RotateObject()
    {
        if (isRotating)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotate(-rotationAngle);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotate(rotationAngle);
        }
    }

    private void Rotate(float angle)
    {
        Vector3 rotationAxisVector = Vector3.zero;
        switch (rotationAxis)
        {
            case RotationAxis.XAxis:
                rotationAxisVector = Vector3.right;
                break;
            case RotationAxis.YAxis:
                rotationAxisVector = Vector3.up;
                break;
            case RotationAxis.ZAxis:
                rotationAxisVector = Vector3.forward;
                break;
        }
        // 更新目标旋转
        targetRotation *= Quaternion.AngleAxis(angle, rotationAxisVector);

        // 标记开始旋转
        isRotating = true;
    }

    void OnMouseDown()
    {
        SelectionManager.Instance.SelectObject(this);
    }

    private void UpdateMaterial()
    {
        // 更新当前对象的材质
        UpdateMaterialsForRenderer(myRenderer);

        // 遍历所有子对象并更新它们的材质
        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                UpdateMaterialsForRenderer(childRenderer);
            }
        }
    }

    private void UpdateMaterialsForRenderer(Renderer renderer)
    {
        Material[] materials = renderer.materials;  // 获取当前材质数组的副本

        if (materials.Length >= 2)
        {
            materials[1] = isSelected ? highlightMaterial : null;
        }

        renderer.materials = materials;  // 重新设置材质数组
    }

    public void InitializeSelectionState()
    {
        isSelected = false; // 确保物体开始时未被选中
        UpdateMaterial(); // 更新材质，反映未选中状态
        SetSecondMaterialToNull(transform); // 调用该方法确保所有材质正确设置
    }

    private void SetSecondMaterialToNull(Transform objTransform)
    {
        Renderer renderer = objTransform.GetComponent<Renderer>();
        if (renderer != null && renderer.materials.Length >= 2)
        {
            Material[] materials = renderer.materials;
            materials[1] = null;  // 将第二材质设置为null
            renderer.materials = materials;
        }

        foreach (Transform child in objTransform)
        {
            SetSecondMaterialToNull(child);  // 递归调用以更新所有子对象
        }
    }

    public void Select()
    {
        isSelected = true;
        UpdateMaterial();
    }

    public void Deselect()
    {
        isSelected = false;
        UpdateMaterial();
    }
}