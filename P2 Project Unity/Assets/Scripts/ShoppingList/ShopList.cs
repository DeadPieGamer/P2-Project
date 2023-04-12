using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    private List<int> shopListIndex = new List<int>();
    //[SerializeField] private WordCardList cardList;

    private void Start()
    {
        
        Setamount = 6;//UnityEngine.Random.Range(3, 10);
        TryOut = new WordCards[Setamount];
        //PlayerPrefs.SetInt("currentDay", -1);
        
        if (PlayerPrefs.GetInt("currentDay", -1) != DateTime.Now.DayOfYear)
        {
            DefineCards();
            setListItem(avaiableSet);
            //string json = JsonUtility.ToJson(shopListIndex,true);
            //File.WriteAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.json" ,json);
            string savedData = String.Join(",",shopListIndex.ToArray());
            File.WriteAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt", savedData);
            PlayerPrefs.SetInt("currentDay", DateTime.Now.DayOfYear);
            Debug.Log(savedData);
            SetComp();
        }
        else
        {
            Debug.Log("SameDate");
            //string json = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.json");
            //shopListIndex = JsonUtility.FromJson<List<int>>(json);
            string savedData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
            shopListIndex = savedData.Split(',').ToList().Select(int.Parse).ToList();
            //string test = String.Join(",", shopListIndex.ToArray());
            //Debug.Log(test);
            DefineCards(shopListIndex);
            SetComp();
        }
        
        
        
        
        
        
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
                    shopListIndex.Add(System.Array.IndexOf<WordCards>(usingSet, newCard));
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
    
    private void DefineCards(List<int> inputlist)
    {
        avaiableSet.Clear();

        GetPreDefinedSet(ST[0]);
        avaiableSet.Add(usingSet[inputlist[0]]);
        avaiableSet.Add(usingSet[inputlist[1]]);

        GetPreDefinedSet(ST[1]);
        avaiableSet.Add(usingSet[inputlist[2]]);
        avaiableSet.Add(usingSet[inputlist[3]]);

        GetPreDefinedSet(ST[2]);
        avaiableSet.Add(usingSet[inputlist[4]]);
        avaiableSet.Add(usingSet[inputlist[5]]);
    }
}
