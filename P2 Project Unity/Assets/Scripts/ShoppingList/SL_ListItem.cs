using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SL_ListItem : MonoBehaviour
{
    private TextMeshProUGUI shopText;
    private Image shopPic;


    private void Awake()
    {
        shopText = GetComponentInChildren<TextMeshProUGUI>();
        shopPic = GetComponentInChildren<Image>();
    }
    public void SetListComp(WordCards card)
    {
        shopText.text = card.danish_Word;
        shopPic.sprite = card.word_Picture;
    }
}
