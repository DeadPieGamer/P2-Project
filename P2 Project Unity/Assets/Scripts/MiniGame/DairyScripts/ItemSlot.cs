using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // code from Code Monkey on youtube, link: https://www.youtube.com/watch?v=BGr-7GZJNXg
    public WordCards shelfItem;
    private WordCards droppedItemId;
    private Sorting_PointChecker checker;

    [SerializeField] AudioSource soundChecker;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;

    CardSetLoader loader;

    int startIndex = 4;
    List<int> wholeArrayInd;
    List<int> ArrayNum = new List<int>();
    List<WordCards> cardSlot = new List<WordCards>();
    private List<bool> LearnedArray = new List<bool>();
    SetTypes gameDeck = SetTypes.Dairy;

    int Setamount = 6;
    private void Start()
    {
        
        loader = GameObject.FindGameObjectWithTag("loader").GetComponent<CardSetLoader>();
        checker = GameObject.FindGameObjectWithTag("checker").GetComponent<Sorting_PointChecker>();
        soundChecker = GameObject.FindGameObjectWithTag("checker").GetComponent<AudioSource>();

        string wordData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
        wholeArrayInd = wordData.Split(',').ToList().Select(int.Parse).ToList();

        for (int i = startIndex; i < startIndex+2; i++)
        {
            ArrayNum.Add(wholeArrayInd[i]);
        }

        for (int i = startIndex-startIndex; i < startIndex-2; i++)
        {
            cardSlot.Add(loader.Get_Set(gameDeck)[ArrayNum[i]]);
        }

        string boolData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
        string[] convertstep = boolData.Split(',').ToArray();
        if (convertstep.Length == 1)
        {
            LearnedArray = new List<bool> { false, false, false, false, false, false };
            string newboolData = String.Join(",", LearnedArray);
            File.WriteAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt", newboolData);
        }
        else
        {
            LearnedArray.Clear();
            for (int i = 0; i < Setamount; i++)
            {
                LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
            }
        }
    }
    

    public bool IsCorrect()
    {
        return shelfItem == droppedItemId;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) // this is the gameobject that is currently being dragged and do a little test to see if it is not null, 
        {
            // To avoid moving already correct items
            if (eventData.pointerDrag.CompareTag("Undrag")) return;

            // then lets take it, get the component of RectTransform and set the anchorposition to this anchored position
            eventData.pointerDrag.GetComponent<RectTransform>() .transform.position = GetComponent<RectTransform>().transform.position; // With This our items should snap into position when we drop it near the itemslot

            var Items = eventData.pointerDrag.GetComponent<Items>();

            if (Items != null)
            {
                droppedItemId = Items.myItem;

                if (IsCorrect())
                {
                    Debug.Log("They're the same");
                    Items.tag = "Undrag";
                    Debug.Log(Items.tag);
                    checker.AddPoints(1);
                    soundChecker.PlayOneShot(correctSound);
                    CheckShoplist(shelfItem);
                    CheckShoplist(shelfItem);
                    CheckShoplist(shelfItem);
                }
                else
                {
                   
                    Debug.Log("They're different");
                    Items.moveBack();
                    soundChecker.PlayOneShot(wrongSound);
                }
                
            }
            

        }
    }
    private void CheckShoplist(WordCards card)
    {
        string boolData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
        string[] convertstep = boolData.Split(',').ToArray();
        LearnedArray.Clear();
        for (int i = 0; i < Setamount; i++)
        {
            LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
        }
        for (int i = startIndex; i < startIndex + 2; i++)
        {
            if (cardSlot[i] == card)
            {
                LearnedArray[i] = true;
                string newboolData = String.Join(",", LearnedArray);
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt", newboolData);
            }
        }
    }
}
