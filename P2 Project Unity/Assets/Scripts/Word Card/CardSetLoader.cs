using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardSetLoader : MonoBehaviour
{
    public WordCards[] cards;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public WordCards Select_RandomCards()
    {
        int randomCard = Random.Range(0, cards.Length);
        return cards[randomCard];
    }
    public void Select_CardSet()
    {
        
    }
}
