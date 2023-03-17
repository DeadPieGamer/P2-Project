using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Connect_Game : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;

    private void Awake()
    {
        wordPicture = GetComponent<Image>();
    }

    public void setSprite(WordCards card)
    {
        wordPicture.sprite = card.word_Picture;
    }
    public void setWord(WordCards card)
    {
        wordText.text = card.danish_Word;
        word_audiosource.clip = card.word_Audio;
    }
}
