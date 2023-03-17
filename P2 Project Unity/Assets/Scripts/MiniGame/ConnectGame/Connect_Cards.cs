using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect_Cards : MonoBehaviour
{
    public CardSetLoader CardSetLoader;
    //public CardDisplay CardDisplay;
    public WordCards[] usingSet;
    public HashSet<WordCards> avaiableSet = new HashSet<WordCards>();

    private void Awake()
    {
        usingSet = CardSetLoader.Get_Set(SetTypes.Meat);

        while (avaiableSet.Count < 4)
        {
            int randomCard = Random.Range(0, usingSet.Length);
            avaiableSet.Add(usingSet[randomCard]);
        }

    }
    private void Start()
    {
        
        //CardDisplay.Get_NewCard();

    }
   
}
