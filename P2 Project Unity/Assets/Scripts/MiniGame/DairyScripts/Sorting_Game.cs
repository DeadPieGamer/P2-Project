using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Sorting_Game : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource wordaudiosource;
    public WordCards AssignedCard;


    private void Awake()
    {

        wordPicture = GetComponent<Image>();
        wordText = GetComponentInChildren<TextMeshProUGUI>();
        wordaudiosource = GetComponent<AudioSource>();
    }

    public void setBottom(WordCards card)
    {
        wordPicture.sprite = card.word_Picture;
        wordaudiosource.clip = card.word_Audio;
        AssignedCard = card;
        GetComponent<Items>().myItem = card;
    }
    public void setTop(WordCards card)
    {
        wordText.text = card.danish_Word;
        wordaudiosource.clip = card.word_Audio;
        AssignedCard = card;
        GetComponent<ItemSlot>().shelfItem = card;
        wordPicture.sprite = card.word_Picture;
    }
    
}
