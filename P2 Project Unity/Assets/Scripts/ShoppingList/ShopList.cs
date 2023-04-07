using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    public WordCards[] usingSet;
    public WordCards[] TryOut;
    private List<WordCards> avaiableSet = new List<WordCards>();
    [SerializeField] private GameObject[] avaiableListIndex;
    [SerializeField] private int Setamount;

    private void Start()
    {
        Setamount = UnityEngine.Random.Range(3, 10);
        TryOut = new WordCards[Setamount];
        for(int i=0;i<Setamount;i++)
        {
            GetRandomSet();
            avaiableSet.Add(CardSetLoader.Select_RandomCards(usingSet));
            
        }
        
        setListItem(avaiableSet);

    }
    private void setListItem(List<WordCards> Input)
    {
        List<WordCards> output = new List<WordCards>();
        output = Input.Distinct().ToList();
        Setamount = output.Count;
        for(int i=0;i<Setamount;i++)
        {
            TryOut[i] = output[i];
            Debug.Log(TryOut[i]);
        }
        
        RemoveNull(TryOut);
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
}
