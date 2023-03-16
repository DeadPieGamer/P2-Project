using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardSetLoader : MonoBehaviour
{
    public WordCards defaultCard;

    public WordCards[] cards = new WordCards[0];

    Dictionary<SetTypes, WordCards[]> CardSet_Dict = new Dictionary<SetTypes, WordCards[]>();
    private void Awake()
    {
        foreach(SetTypes folder in Enum.GetValues(typeof(SetTypes)))
        {
            CardSet_Dict.Add(folder, Resources.LoadAll("WordCards_Folder/" + folder.ToString(), typeof(WordCards)).Cast<WordCards>().ToArray());
            
        }

        //Select_CardSet(SetTypes.Test);
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
        if (cards.Length == 0)
        {
            return defaultCard;
        }

        int randomCard = UnityEngine.Random.Range(0, cards.Length);
        return cards[randomCard];
    }

    public void Select_CardSet()
    {
        SetTypes randomSet = (SetTypes)UnityEngine.Random.Range(0, CardSet_Dict.Count);
        cards = CardSet_Dict[randomSet];
    }
    
    public void Select_CardSet(SetTypes Deck)
    {
        cards = CardSet_Dict[Deck];

    }

    public void Select_CardSet(Felix.Variables.DummyCardSet Deck)
    {
        cards = CardSet_Dict[Deck.SetTypeEnum];

    }

    public void Select_CardSet(int Deck)
    {
        cards = CardSet_Dict[(SetTypes)Deck];

    }
}
