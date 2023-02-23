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
        wordText.text = card.danish_Word;
        wordPicture.sprite = card.word_Picture;
        word_audiosource.clip = card.word_Audio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Audio_play()
    {
        word_audiosource.Play();
    }
}

