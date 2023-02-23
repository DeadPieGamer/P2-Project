using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Word Card")]
public class WordCards : ScriptableObject
{
    [SerializeField, Tooltip("Word of the image in Danish")] public string danish_Word;
    [SerializeField, Tooltip("Image of the word")] public Sprite word_Picture;
    [SerializeField, Tooltip("Pronouciation of the word")] public AudioClip word_Audio;

    
}
