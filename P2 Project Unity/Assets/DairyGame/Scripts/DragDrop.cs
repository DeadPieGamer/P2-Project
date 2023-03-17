using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler

// code from Code Mokey on youtube, link: https://www.youtube.com/watch?v=BGr-7GZJNXg

{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent < RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData) // This one gets called when we begin dragging the item
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) // This is what gets called on every frame whilst we are dragging the item and the mouse has moved
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta; // This field contains the movement data, which the amout that the mouse moved since the previous frame, so by adding this we wont be moving our object alongside the mouse
    }

    public void OnEndDrag(PointerEventData eventData) // This one gets called when we stop dragging the item
    {
        Debug.Log("OnEndDrag");
    }

    public void OnPointerDown(PointerEventData eventData) //Which will be called when the mouse is pressed, whilst on top of this object
    {
        Debug.Log("OnPointerDown");
    }
}
   
