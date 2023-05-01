using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Connect_Cards : MonoBehaviour
{
    //some sections of this script was inspried mostly by these 2 video
    //https://youtu.be/n2cYUbtt28M by Smart Pengiuns
    //https://youtu.be/bILijwzJrZg by Redefine Gamedev
    //public Connect_Game Connect_Game;

    public CardSetLoader CardSetLoader;
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
    //This function was inspired by an answer from stackoverflow
    //https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
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
