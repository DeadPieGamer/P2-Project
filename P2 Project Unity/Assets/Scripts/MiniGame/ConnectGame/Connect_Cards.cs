using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Connect_Cards : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    //public Connect_Game Connect_Game;
    public WordCards[] usingSet;
    public List<WordCards> avaiableSet = new List<WordCards>();

    [SerializeField] private GameObject[] avaiableLeftIndex;
    [SerializeField] private GameObject[] avaiableRightIndex;

    private void Awake()
    {
       

    }
    private void Start()
    {
        usingSet = CardSetLoader.Get_Set(SetTypes.Meat);

        while (avaiableSet.Count < 4)
        {
            int randomCard = UnityEngine.Random.Range(0, usingSet.Length);

            if(!avaiableSet.Contains(usingSet[randomCard]))
            {
                avaiableSet.Add(usingSet[randomCard]);
            }
        }

       for (int i = 0; i < avaiableSet.Count; i++)
        {
            avaiableLeftIndex[i].GetComponent<Connect_Game>().setSprite(avaiableSet[i]);
        }
    }
    
}
