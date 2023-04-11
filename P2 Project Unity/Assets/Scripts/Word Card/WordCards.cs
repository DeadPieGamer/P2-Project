using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "New Word", menuName = "Word Card")] //Creating an instance inside the create menu in unity inspector

public class WordCards : ScriptableObject //using ScriptableObject instead of monobehavior since we're not editing a gameobject directly
{
    [Header("Card Elements")]
    [SerializeField, Tooltip("Word of the image in Danish")] public string danish_Word; 
    [SerializeField, Tooltip("Image of the word")] public Sprite word_Picture; 
    [SerializeField, Tooltip("Pronouciation of the word")] private AudioClip[] word_Audioclips;

    [HideInInspector, Tooltip("Returns one of the word audio clips")] public AudioClip word_Audio
    {
        get { return word_Audioclips[UnityEngine.Random.Range(0, word_Audioclips.Length)]; }
    }
}
