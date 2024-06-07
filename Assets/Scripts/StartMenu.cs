using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public Animator menuAnimator; // 用于菜单动画的 Animator 组件
    public GameObject menu;
    public GameObject globalSound;
    public GameObject skull;


    // 可以添加其他方法来处理菜单选项，例如开始游戏，设置等
    public void StartGame()
    {
        menuAnimator.SetTrigger("Start");
        globalSound.SetActive(true);
        skull.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}