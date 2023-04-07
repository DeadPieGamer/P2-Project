using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    public WordCards[] usingSet;
    public WordCards[] TryOut;
    private HashSet<WordCards> avaiableSet = new HashSet<WordCards>();
    [SerializeField] private GameObject[] avaiableListIndex;
    [SerializeField] private int Setamount = 4;

    private void Start()
    {
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
    }
    
    private void GetRandomSet()
    {
        int randomSet = Random.Range(1, CardSetLoader.CardSet_Dict.Count);
        usingSet = CardSetLoader.Get_Set(randomSet);
    }
}
