using System.Collections;
using System.Collections.Generic;
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


    private void Start()
    {
        checker = GameObject.FindGameObjectWithTag("checker").GetComponent<Sorting_PointChecker>();
        soundChecker = GameObject.FindGameObjectWithTag("checker").GetComponent<AudioSource>();
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
}
