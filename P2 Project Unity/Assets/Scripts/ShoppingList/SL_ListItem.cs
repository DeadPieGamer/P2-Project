using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SL_ListItem : MonoBehaviour
{
    private TextMeshProUGUI shopText;
    private Image shopPic;

    public WordCards itemCard;
    public SetTypes setType;
    private void Awake()
    {
        shopText = GetComponentInChildren<TextMeshProUGUI>();
        shopPic = GetComponentInChildren<Image>();
    }
    public void SetListComp(WordCards card)
    {
        shopText.text = card.danish_Word;
        shopPic.sprite = card.word_Picture;
        itemCard = card;

    }
    public void setDeck(SetTypes Deck)
    {
        setType = Deck;
    }
}
