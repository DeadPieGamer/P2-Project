using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // code from Code Monkey on youtube, link: https://www.youtube.com/watch?v=BGr-7GZJNXg
    public WordCards shelfItem;

    private WordCards droppedItemId;

    private Vector2 startPos;

    private int points = 0;

    public bool IsCorrect()
    {
         return shelfItem == droppedItemId;
    }

    private void Update()
    {
        if (points >= 3)
        {
            Debug.Log("You won");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) // this is the gameobject that is currently being dragged and do a little test to see if it is not null, 
        {
            if (CompareTag("Undrag")) return;

            // then lets take it, get the component of RectTransform and set the anchorposition to this anchored position
            eventData.pointerDrag.GetComponent<RectTransform>() .anchoredPosition = GetComponent<RectTransform>().anchoredPosition; // With This our items should snap into position when we drop it near the itemslot

            var Items = eventData.pointerDrag.GetComponent<Items>();

            if (Items != null)
            {
                droppedItemId = Items.myItem;
                if (IsCorrect())
                {
                    Debug.Log("They're the same");
                    Items.tag = "Undrag";
                    Debug.Log(Items.tag);
                    points += 1;
                }
                else
                {
                    Debug.Log("They're different");
                    Items.moveBack();
                }
            }
            else
            {
                Debug.LogError("There was no letter attached to the dropped item!");
            }

        }
        
    }
}
