using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler

// code from Code Mokey on youtube, link: https://www.youtube.com/watch?v=BGr-7GZJNXg

{
    private RectTransform rectTransform; // Grab our RectTransform component
    private CanvasGroup canvasGroup; // Grabbing our Canvas Group component

    private void Awake()
    {
        rectTransform = GetComponent < RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData) // This one gets called when we begin dragging the item
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f; // makes the item slightly transparent whilst it is being dragged, by modifying the alpha
        canvasGroup.blocksRaycasts = false; // sets the blocksraycast into false so that the raycast goes through this object and lands on the item slot
    }

    public void OnDrag(PointerEventData eventData) // This is what gets called on every frame whilst we are dragging the item and the mouse has moved
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta; // This field contains the movement data, which the amout that the mouse moved since the previous frame, so by adding this we wont be moving our object alongside the mouse
    }

    public void OnEndDrag(PointerEventData eventData) // This one gets called when we stop dragging the item
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; // When we finish dragging we reset the alpha back to one
        canvasGroup.blocksRaycasts = true; // When we finish dragging we set it back to true
    }

    public void OnPointerDown(PointerEventData eventData) //Which will be called when the mouse is pressed, whilst on top of this object
    {
        Debug.Log("OnPointerDown");
    }
}
   

