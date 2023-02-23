using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public WordCards card;

    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        Input_newCard(card);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Audio_play()
    {
        word_audiosource.Play();
    }

    public void Input_newCard(WordCards newcard)
    {
        wordText.text = newcard.danish_Word;
        wordPicture.sprite = newcard.word_Picture;
        word_audiosource.clip = newcard.word_Audio;
    }
}

