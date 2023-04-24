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
    public List<WordCards> avaiableSet = new List<WordCards>();
    [SerializeField] private GameObject[] avaiableListIndex;
    [SerializeField] private int Setamount;
    private SetTypes[] ST = { SetTypes.Meat,SetTypes.FruitsAndGreens,SetTypes.Dairy};
    private List<bool> LearnedArray= new List<bool>();
    private List<int> shopListIndex = new List<int>();
    //[SerializeField] private WordCardList cardList;


    
    private void Start()
    {
        
        Setamount = 6;//UnityEngine.Random.Range(3, 10);
        TryOut = new WordCards[Setamount];
        //PlayerPrefs.SetInt("currentDay", -1);
        
        if (PlayerPrefs.GetInt("currentDay", -1) != DateTime.Now.DayOfYear)
        {
            PlayerPrefs.SetInt("firstPass", 0);
            DefineCards();
            setListItem(avaiableSet);
            LearnedArray = new List<bool> { false, false, false, false, false, false };
            string wordData = String.Join(",", shopListIndex.ToArray());
            File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/cardDatafile.txt", wordData);
            string boolData = String.Join(",", LearnedArray);
            File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/boolDatafile.txt", boolData);
            PlayerPrefs.SetInt("currentDay", DateTime.Now.DayOfYear);
            SetComp();
        }
        else
        {

            string wordData = File.ReadAllText(Application.persistentDataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
            if (wordData == "")
            {
                DefineCards();
                setListItem(avaiableSet);
                string newwordData = String.Join(",", shopListIndex.ToArray());
                File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/cardDatafile.txt", newwordData);
                PlayerPrefs.SetInt("currentDay", DateTime.Now.DayOfYear);
                SetComp();
            }
            else shopListIndex = wordData.Split(',').ToList().Select(int.Parse).ToList();
            
            string boolData = File.ReadAllText(Application.persistentDataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
            string[] convertstep = boolData.Split(',').ToArray();
            if(convertstep.Length <= 1 )
            {
                LearnedArray = new List<bool> { false, false, false, false, false, false };
                string newboolData = String.Join(",", LearnedArray);
                File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/boolDatafile.txt", newboolData);
            }
            else
            {
                LearnedArray.Clear();
                for (int i = 0; i < Setamount; i++)
                {
                    LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
                }
            }
            //string test = String.Join(",", shopListIndex.ToArray());
            Debug.Log(LearnedArray.ToString());
            DefineCards(shopListIndex);
            SetComp();
        }
        
        
        
        
        
        
    }
    private void setListItem(List<WordCards> Input)
    {
        List<WordCards> output = new List<WordCards>();
        
        output = Input;
        
        Setamount = output.Count;
        for(int i=0;i<Setamount;i++)
        {
            TryOut[i] = output[i];
            
        }
        
        
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
            if (LearnedArray.Count <= 1)
            {
                LearnedArray = new List<bool> { false, false, false, false, false, false };
                avaiableListIndex[i].GetComponent<SL_ListItem>().Striket(LearnedArray[i]);
            }
            else
            {
                avaiableListIndex[i].GetComponent<SL_ListItem>().Striket(LearnedArray[i]);
            }
            avaiableListIndex[i].GetComponent<SL_ListItem>().setDeck(ST[i/2]);
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

   
    //private void Update()
    //{
    //    SetComp();
    //}
}
