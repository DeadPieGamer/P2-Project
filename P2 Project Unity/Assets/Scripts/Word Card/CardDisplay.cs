using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class CardDisplay : MonoBehaviour
{
    public WordCards card;
    public WordCards[] cards;
    public TextMeshProUGUI wordText;
    public Image wordPicture;
    public AudioSource word_audiosource;

    public WordCards[] TestSet;
    public WordCards[] DairySet;
    public WordCards[] AnimalSet;
    public WordCards[] VegetableSet;
    public WordCards[] FruitSet;
    public WordCards[] DrinkSet;
    public WordCards[] BakeSet;
    public WordCards[] AsianSet;
    public WordCards[] CannedSet;
    public WordCards[] SnackSet;
    public WordCards[] CleaningSet;

    private void Awake()
    {
        TestSet = Resources.LoadAll("WordCards_Folder/Test", typeof(WordCards)).Cast<WordCards>().ToArray();//Call Word card folder using Resouces.LoadALL("Name of the folder", typeof(scriptableobjectname))
        DairySet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        AnimalSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        VegetableSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        FruitSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        DrinkSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        BakeSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        AsianSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        CannedSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        SnackSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        CleaningSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        cards = TestSet;
        Input_newCard(card);
        Debug.Log(cards.Length);
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
    public void Select_CardSet()
    {
       
    }
}

