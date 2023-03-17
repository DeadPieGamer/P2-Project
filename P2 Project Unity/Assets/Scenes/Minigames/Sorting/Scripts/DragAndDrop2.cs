using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop2 : MonoBehaviour
{ 
    private Vector3 mOffset;
    private float mZCoord;

    private bool mIsDragging = false;

    private Vector3 mStartPosition;

    public string mCorrectWord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        mIsDragging = true;

        mStartPosition = transform.position;
    }

    private void OnMouseUp()
    {
        mIsDragging = false;

        if (transform.position == mStartPosition)
        {
            // Picture was not dropped on a word, so reset its position
            transform.position = mStartPosition;
        }
    }

    private void OnMouseDrag()
    {
        if (mIsDragging)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Word") && other.gameObject.name == mCorrectWord)
        {
            // Snap picture to word position
            transform.position = other.transform.position;
        }
        else
        {
            // Picture was dropped on the wrong word, so reset its position
            transform.position = mStartPosition;
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


}


