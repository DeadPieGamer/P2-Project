using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardTest : MonoBehaviour
{
    public CardSetLoader CardLoader;
    public WordCards[] Set;
    public WordCards card;
    public WordCards testCard;

    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;
    // Start is called before the first frame update
    void Start()
    {
        Set = CardLoader.Get_Set(SetTypes.Dairy);
        card = Set[1];

        

        testCard = Set[System.Array.IndexOf<WordCards>(Set,card)];

        Input_newCard(testCard);
    }

    public void Input_newCard(WordCards newcard)
    {
        wordText.text = newcard.danish_Word;
        wordPicture.sprite = newcard.word_Picture;
        word_audiosource.clip = newcard.word_Audio;
    }
}
