using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting_Falling : MonoBehaviour
{
    public RectTransform uiImage;
    public RectTransform itemSlot;

    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = uiImage.anchoredPosition;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void MoveImage(Vector2 offset)
    {
        uiImage.anchoredPosition += offset;

        // Check if the UI image is in the correct item slot
        if (uiImage.anchoredPosition == itemSlot.anchoredPosition)
        {
            // Do nothing, the image is in the correct item slot
        }
        else
        {
            // Reset the UI image to its starting position
            uiImage.anchoredPosition = startingPosition;
        }
    }

}
