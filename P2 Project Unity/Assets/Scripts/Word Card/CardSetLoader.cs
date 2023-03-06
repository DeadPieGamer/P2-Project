using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardSetLoader : MonoBehaviour
{
    public WordCards[] cards;

    public WordCards[] TestSet;
    public WordCards[] AnimalSet;
    public WordCards[] AsianSet;
    public WordCards[] BakeSet;
    public WordCards[] CannedSet;
    public WordCards[] CleaningSet;
    public WordCards[] DairySet;
    public WordCards[] DrinkSet;
    public WordCards[] FruitSet;
    public WordCards[] SnackSet;
    public WordCards[] VegetableSet;

    Dictionary<SetTypes, WordCards[]> CardSet_Dict = new Dictionary<SetTypes, WordCards[]>();
    private void Awake()
    {
        foreach(SetTypes folder in Enum.GetValues(typeof(SetTypes)))
        {
            CardSet_Dict.Add(folder, Resources.LoadAll("WordCards_Folder/" + folder.ToString(), typeof(WordCards)).Cast<WordCards>().ToArray());
            
        }
        //TestSet = Resources.LoadAll("WordCards_Folder/Test", typeof(WordCards)).Cast<WordCards>().ToArray();//Call Word card folder using Resouces.LoadALL("Name of the folder", typeof(scriptableobjectname))
        //DairySet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //AnimalSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //VegetableSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //FruitSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //DrinkSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //BakeSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //AsianSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //CannedSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //SnackSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();
        //CleaningSet = Resources.LoadAll("WordCards_Folder/Dairy", typeof(WordCards)).Cast<WordCards>().ToArray();

        TestSet = CardSet_Dict[SetTypes.Test];
    }



    void Start()
    {
        Select_CardSet("Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public WordCards Select_RandomCards()
    {
        int randomCard = UnityEngine.Random.Range(0, cards.Length);
        return cards[randomCard];
    }
    public void Select_CardSet()
    {
        SetTypes randomSet = (SetTypes)UnityEngine.Random.Range(0, CardSet_Dict.Count);
        cards = CardSet_Dict[randomSet];
    }
    public void Select_CardSet(string Deck)
    {
        switch(Deck)
        {
            case "Animal":
                cards = AnimalSet;
                break ;
            case "Asian":
                cards = AsianSet;
                break;
            case "Bake":
                cards = BakeSet;
                break;
            case "Canned":
                cards = CannedSet;
                break;
            case "Cleaning":
                cards = CleaningSet;
                break;
            case "Dairy":
                cards = DairySet;
                break;
            case "Drink":
                cards = DrinkSet;
                break;
            case "Fruit":
                cards = FruitSet;
                break;
            case "Snack":
                cards = SnackSet;
                break;
            case "Vegetable":
                cards = VegetableSet;
                break;
            default:
                cards = TestSet;
                break;
        }

            
    }
}
