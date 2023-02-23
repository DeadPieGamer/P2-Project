using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardDisplay : MonoBehaviour
{
    public WordCards card;

    public WordCards[] cards;

    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        Input_newCard(card);
        cards = (WordCards[])Resources.LoadAll("WordCards", typeof(WordCards));//Call Word card folder using Resouces.LoadALL("Name of the folder", typeof(scriptableobjectname))
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Select_RandomCards()
    {
        int randomCard = Random.Range(0, cards.Length);
        Input_newCard(cards[randomCard]);
    }
}

