using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsText : MonoBehaviour
{
    public GameObject paperText;
    public GameObject paperImage;

    public void ShowPaperText()
    {
        paperText.SetActive(true);
    }

    public void ShowPaperImage()
    {
        paperImage.SetActive(true);
    }
}