using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.IO;

public class Cashier_WordLoader : MonoBehaviour
{
    public List<WordCards> avaiableSet;
    public List<int> shopListIndex = new List<int>();
    [SerializeField]private CardSetLoader CardSetLoader;

    private WordCards[] usingSet;
    private SetTypes[] ST = { SetTypes.Meat, SetTypes.FruitsAndGreens, SetTypes.Dairy };
    // Start is called before the first frame update
    void Start()
    {
        string savedData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
        shopListIndex = savedData.Split(',').ToList().Select(int.Parse).ToList();
        DefineCards(shopListIndex);
    }

    private void GetPreDefinedSet(SetTypes Deck)
    {
        usingSet = CardSetLoader.Get_Set(Deck);
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
}
