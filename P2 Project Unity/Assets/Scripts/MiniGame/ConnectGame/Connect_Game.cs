using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Connect_Game : MonoBehaviour
{
    public TextMeshPro wordText;
    public SpriteRenderer wordPicture;
    public AudioSource wordaudiosource;
    public WordCards AssignedCard;
    //public SpriteRenderer connectorSprite;

    private void Awake()
    {

        //connectorSprite = GetComponent<SpriteRenderer>();
        wordText = GetComponent<TextMeshPro>();
        wordaudiosource = GetComponent<AudioSource>();

    }

    public void setSprite(WordCards card)
    {
        wordPicture.sprite = card.word_Picture;
        wordaudiosource.clip = card.word_Audio;
        //connectorSprite.sprite = card.word_Picture;
        AssignedCard = card;
        GetComponent<KO_Draggable>().myCard = card;
    }
    public void setWord(WordCards card)
    {
        wordText.text = card.danish_Word;
        AssignedCard = card;
        GetComponent<KO_Draggable>().myCard = card;
    }
}
