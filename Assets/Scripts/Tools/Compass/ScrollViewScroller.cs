using UnityEngine;
using UnityEngine.UI;

public class ScrollViewScroller : MonoBehaviour
{
    public ScrollRect scrollRect; // 指向ScrollRect组件
    public float scrollSpeed = 0.1f; // 滚动速度或滚动的距离单位
    public GameObject object1; // 第一个对象
    public GameObject object2; // 第二个对象
    public Material specificMaterial; // 特定材质

    void Start()
    {
        // 禁止通过鼠标拖动进行水平滚动
        if (scrollRect != null)
        {
            scrollRect.horizontal = false; // 禁用水平拖动
        }
    }

    // 向左滚动内容
    public void ScrollLeft()
    {
        if (scrollRect != null)
        {
            // 减少水平normalized位置
            scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            // 确保滚动位置不会小于0
            if (scrollRect.horizontalNormalizedPosition < 0)
                scrollRect.horizontalNormalizedPosition = 0;

            // 将两个对象的第五个材质设置为null
            SetMaterial(null);
        }
    }

    // 向右滚动内容
    public void ScrollRight()
    {
        if (scrollRect != null)
        {
            // 增加水平normalized位置
            scrollRect.horizontalNormalizedPosition += scrollSpeed;
            // 确保滚动位置不会超过1
            if (scrollRect.horizontalNormalizedPosition > 1)
                scrollRect.horizontalNormalizedPosition = 1;

            // 将两个对象的第五个材质设置为specificMaterial
            SetMaterial(specificMaterial);
        }
    }

    void SetMaterial(Material mat)
    {
        // 检查对象是否存在并且具有足够的材质
        if (object1 && object1.GetComponent<Renderer>().materials.Length >= 5)
        {
            var materials1 = object1.GetComponent<Renderer>().materials;
            materials1[4] = mat; // 第五个材质的索引是4
            object1.GetComponent<Renderer>().materials = materials1;
        }
        if (object2 && object2.GetComponent<Renderer>().materials.Length >= 5)
        {
            var materials2 = object2.GetComponent<Renderer>().materials;
            materials2[4] = mat; // 第五个材质的索引是4
            object2.GetComponent<Renderer>().materials = materials2;
        }
    }
}