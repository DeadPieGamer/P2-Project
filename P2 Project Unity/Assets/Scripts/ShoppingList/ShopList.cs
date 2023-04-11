using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    public WordCards[] usingSet;
    public WordCards[] TryOut;
    private List<WordCards> avaiableSet = new List<WordCards>();
    [SerializeField] private GameObject[] avaiableListIndex;
    [SerializeField] private int Setamount;
    private SetTypes[] ST = { SetTypes.Meat,SetTypes.FruitsAndGreens,SetTypes.Dairy};

    private TextMeshProUGUI[] shopText;
    private Image[] shopPic;

    private void Start()
    {
        Setamount = 6;//UnityEngine.Random.Range(3, 10);
        TryOut = new WordCards[Setamount];
        DefineCards();
        setListItem(avaiableSet);
        AssignListItem();
    }
    private void setListItem(List<WordCards> Input)
    {
        List<WordCards> output = new List<WordCards>();
        //output = Input.Distinct().ToList();
        output = Input;
        
        Setamount = output.Count;
        for(int i=0;i<Setamount;i++)
        {
            TryOut[i] = output[i];
            Debug.Log(TryOut[i]);
        }
        
        //RemoveNull(TryOut);
    }
    private void RemoveNull(WordCards[] input)
    {
        
        input = input.Where(c => c != null).ToArray();
    }
    private void GetRandomSet()
    {
        int randomSet = UnityEngine.Random.Range(1, CardSetLoader.CardSet_Dict.Count);
        usingSet = CardSetLoader.Get_Set(randomSet);
    }
    private void GetPreDefinedSet(SetTypes Deck)
    {
        usingSet = CardSetLoader.Get_Set(Deck);
    }
    private void DefineCards()
    {
        for (int j = 0; j < Setamount / 2; j++)
        {
            GetPreDefinedSet(ST[j]);
            for (int i = 0; i < Setamount / 3; i++)
            {
                //GetRandomSet();

                WordCards newCard = CardSetLoader.Select_RandomCards(usingSet);

                if (avaiableSet.Contains(newCard))
                {
                    i--;
                }
                else
                {
                    avaiableSet.Add(newCard);
                }
            }
        }
    }

    private void AssignListItem()
    {
        for (int i = 0; i < Setamount; i++)
        {
            shopText[i] = avaiableListIndex[i].GetComponent<TextMeshProUGUI>();
            shopPic[i] = avaiableListIndex[i].GetComponent<Image>();
        }
    }
    private void AddCompListItem()
    {
        for (int i = 0; i < Setamount; i++)
        {
            shopText[i].text = avaiableSet[i].danish_Word;
            shopPic[i].sprite = avaiableSet[i].word_Picture;
        }
    }
}
