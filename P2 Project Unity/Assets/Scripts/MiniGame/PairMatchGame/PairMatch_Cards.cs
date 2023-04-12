using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PairMatch_Cards : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    //public Connect_Game Connect_Game;
    public WordCards[] usingSet;
    public List<WordCards> avaiableSet = new List<WordCards>();

    [SerializeField] private GameObject[] avaiableBottomIndex;
    //[SerializeField] private GameObject[] avaiableTopIndex;
    [SerializeField] private int Setamount = 4;

    private void Awake()
    {
        //avaiableTopIndex = Shuffle_Array(avaiableTopIndex);
        avaiableBottomIndex = Shuffle_Array(avaiableBottomIndex);

    }
    private void Start()
    {
        usingSet = CardSetLoader.Get_Set(SetTypes.FruitsAndGreens);

        while (avaiableSet.Count < Setamount)
        {
            int randomCard = Random.Range(0, usingSet.Length);

            if (!avaiableSet.Contains(usingSet[randomCard]))
            {
                avaiableSet.Add(usingSet[randomCard]);
            }
        }

        for (int i = 0; i < Setamount; i++)
        {
            avaiableBottomIndex[i].GetComponent<Sorting_Game>().setBottom(avaiableSet[i]);
            avaiableBottomIndex[i + 4].GetComponent<Sorting_Game>().setBottom(avaiableSet[i]);
            //avaiableTopIndex[i].GetComponent<Sorting_Game>().setTop(avaiableSet[i]);
            //avaiableTopIndex[i + 3].GetComponent<Sorting_Game>().setTop(avaiableSet[i]);
        }
    }
    private GameObject[] Shuffle_Array(GameObject[] inputarray)
    {
        GameObject Temp_array;
        for (int i = 0; i < inputarray.Length; i++)
        {
            int rnd = Random.Range(0, inputarray.Length);
            Temp_array = inputarray[rnd];
            inputarray[rnd] = inputarray[i];
            inputarray[i] = Temp_array;
        }
        return inputarray;
    }
}
