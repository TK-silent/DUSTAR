using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MounmentAnimation : MonoBehaviour
{
    public Animator targetAnimator; // 目标动画控制器的引用
    public Material newSecondMaterial; // 要应用的新材质
    public GameObject targetObject; // 要更改材质的根对象

    void Start()
    {
        // 检查是否已正确设置Animator引用和新材质
        if (targetAnimator == null)
        {
            Debug.LogError("Target Animator is not assigned.");
        }
        if (newSecondMaterial == null)
        {
            Debug.LogError("New second material is not assigned.");
        }
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 激活目标对象的启动动画
            targetAnimator.SetTrigger("On");

            // 启动更改材质的协程
            StartCoroutine(ChangeMaterialWithDelay(targetObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 激活目标对象的关闭动画
            targetAnimator.SetTrigger("Off");

            // 启动重置材质的协程
            StartCoroutine(ResetMaterialWithDelay(targetObject));
        }
    }

    // 更改材质的协程
    IEnumerator ChangeMaterialWithDelay(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            Material[] materials = objRenderer.materials;
            if (materials.Length > 1) // 确保有第二个材质
            {
                materials[1] = newSecondMaterial;
                objRenderer.materials = materials; // 应用更改
            }
            else
            {
                Debug.LogWarning("Object does not have a second material: " + obj.name);
            }
        }

        // 等待0.05秒
        yield return new WaitForSeconds(0.02f);

        // 对所有子对象递归调用此协程
        foreach (Transform child in obj.transform)
        {
            yield return StartCoroutine(ChangeMaterialWithDelay(child.gameObject));
        }
    }

    // 重置材质的协程
    IEnumerator ResetMaterialWithDelay(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            Material[] materials = objRenderer.materials;
            if (materials.Length > 1) // 确保有第二个材质
            {
                materials[1] = null;
                objRenderer.materials = materials; // 应用更改
            }
            else
            {
                Debug.LogWarning("Object does not have a second material to reset: " + obj.name);
            }
        }

        // 等待0.05秒
        yield return new WaitForSeconds(0.05f);

        // 对所有子对象递归调用此协程
        foreach (Transform child in obj.transform)
        {
            yield return StartCoroutine(ResetMaterialWithDelay(child.gameObject));
        }
    }
}
