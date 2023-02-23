using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Word Card")] //Creating an instance inside the create menu in unity inspector

public class WordCards : ScriptableObject //using ScriptableObject instead of monobehavior since we're not editing a gameobject directly
{
    [Header("Card Elements")]
    [SerializeField, Tooltip("Word of the image in Danish")] public string danish_Word; 
    [SerializeField, Tooltip("Image of the word")] public Sprite word_Picture; 
    [SerializeField, Tooltip("Pronouciation of the word")] public AudioClip word_Audio;
}
