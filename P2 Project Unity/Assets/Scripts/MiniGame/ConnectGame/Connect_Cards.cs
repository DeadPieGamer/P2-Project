using Microsoft.Unity.VisualStudio.Editor;
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
    [SerializeField] private int Setamount = 4;

    private void Awake()
    {
       avaiableRightIndex = Shuffle_Array(avaiableRightIndex);
       avaiableLeftIndex = Shuffle_Array(avaiableLeftIndex);
    }
    private void Start()
    {
        usingSet = CardSetLoader.Get_Set(SetTypes.Meat);

        while (avaiableSet.Count < Setamount)
        {
            int randomCard = Random.Range(0, usingSet.Length);

            if(!avaiableSet.Contains(usingSet[randomCard]))
            {
                avaiableSet.Add(usingSet[randomCard]);
            }
        }

       for (int i = 0; i < Setamount; i++)
        {
            avaiableLeftIndex[i].GetComponent<Connect_Game>().setSprite(avaiableSet[i]);
            avaiableRightIndex[i].GetComponent<Connect_Game>().setWord(avaiableSet[i]);
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
