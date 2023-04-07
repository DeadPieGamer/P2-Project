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
    private HashSet<WordCards> avaiableSet = new HashSet<WordCards>();
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
    private void setListItem(HashSet<WordCards> Input)
    {
        
        foreach(WordCards card in Input)
        {
            Input.CopyTo(TryOut);
        }
        for(int i=0;i<Setamount;i++)
        {
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
