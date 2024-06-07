using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    public TMP_Text tooltipText;
    public GameObject tooltipObject;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowTooltip(string text)
    {
        tooltipText.text = text;
        tooltipObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}