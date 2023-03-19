using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) // this is the gameobject that is currently being dragged and do a little test to see if it is not null, 
        {                                  // then lets take it, get the component of RectTransform and set the anchorposition to this anchored position
            eventData.pointerDrag.GetComponent<RectTransform>() .anchoredPosition = GetComponent<RectTransform>().anchoredPosition; 
        }                                  // With This our items should snap into position when we drop it near the itemslot
    }
}
