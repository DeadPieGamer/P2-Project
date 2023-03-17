using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect_Cards : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    //public CardDisplay CardDisplay;
    public WordCards[] usingSet;
    public HashSet<WordCards> avaiableSet = new HashSet<WordCards>();

    [SerializeField] private GameObject[] avaiableLeftIndex;
    [SerializeField] private GameObject[] avaiableRightIndex;

    private void Awake()
    {
        usingSet = CardSetLoader.Get_Set(SetTypes.Meat);

        while (avaiableSet.Count < 4)
        {
            int randomCard = UnityEngine.Random.Range(0, usingSet.Length);
            avaiableSet.Add(usingSet[randomCard]);
        }

    }
    private void Start()
    {

        

    }
   
}
