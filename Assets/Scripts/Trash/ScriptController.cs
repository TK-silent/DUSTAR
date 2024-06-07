using UnityEngine;

public class ScriptController : MonoBehaviour
{
    public MonoBehaviour[] scriptsToControl;  // 需要控制的脚本数组

    // 禁用所有脚本
    public void DisableScripts()
    {
        foreach (var script in scriptsToControl)
        {
            script.enabled = false;
        }
    }

    // 启用所有脚本
    public void EnableScripts()
    {
        foreach (var script in scriptsToControl)
        {
            script.enabled = true;
        }
    }
}