using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Connect_Game : MonoBehaviour
{
    public Image cardImage;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
    }

    public void setImage(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }
}
