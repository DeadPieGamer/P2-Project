using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    string json;

    private void Start()
    {
        
        Setamount = 6;//UnityEngine.Random.Range(3, 10);
        TryOut = new WordCards[Setamount];
        //PlayerPrefs.SetInt("currentDay", -1);

        if (PlayerPrefs.GetInt("currentDay", -1) != DateTime.Now.DayOfYear)
        {
            DefineCards();
            setListItem(avaiableSet);
            json = JsonUtility.ToJson(avaiableSet,true);
            File.WriteAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.json" ,json);
            PlayerPrefs.SetInt("currentDay", DateTime.Now.DayOfYear);
        }
        else
        {
            Debug.Log("SameDate");
            json = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.json");
            avaiableSet = JsonUtility.FromJson<List<WordCards>>(json);
        }
        
        SetComp();
        
        
        
        
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
            //Debug.Log(TryOut[i]);
        }
        
        //RemoveNull(TryOut);
    }
    //private void RemoveNull(WordCards[] input)
    //{
        
    //    input = input.Where(c => c != null).ToArray();
    //}
    //private void GetRandomSet()
    //{
    //    int randomSet = UnityEngine.Random.Range(1, CardSetLoader.CardSet_Dict.Count);
    //    usingSet = CardSetLoader.Get_Set(randomSet);
    //}
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
    private void SetComp()
    {
        for(int i = 0; i < Setamount; i++)
        {
            avaiableListIndex[i].GetComponent<SL_ListItem>().SetListComp(avaiableSet[i]);
        }
        
    }
    
}
