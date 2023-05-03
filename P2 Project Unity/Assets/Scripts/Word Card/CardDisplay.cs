using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;
    public CardSetLoader loader;

    //public CardSetLoader loader;
    public WordCards card;

    void Start()
    {
        Get_NewCard();
    }

    /// <summary>
    /// Plays audio of the card using the audio clip from WordCard class
    /// </summary>
    public void Audio_play()
    {
        word_audiosource.Play();
    }

    /// <summary>
    /// A function that loads a new card replacing the defualt values. Text, Image & Audio.
    /// </summary>
    /// <param name="newcard"></param>
    public void Input_newCard(WordCards newcard)
    {
        wordText.text = newcard.danish_Word;
        wordPicture.sprite = newcard.word_Picture;
        word_audiosource.clip = newcard.word_Audio;
    }
    public void Get_NewCard()
    {
        card = loader.Select_RandomCards();
        Input_newCard(card);
    }

}

