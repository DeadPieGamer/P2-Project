using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;

    private bool _isDragActive = false;

    private Vector2 _screenPosition;

    private Vector3 _worldPosition; // for our effective final position in the game

    private Draggable _lastDragged; // reference to our last draggable item


    private void Awake() // we are gonna use awake to make sure that there is only one DragController in the scene
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }
    void Update() 
    {
        if(_isDragActive && (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))) // Check if we're in drag mode and we want to drop. Either when the
        {                                                                                                                        // mouse goes up or we have one touch and the touch phase equals to
                                                                                                                                 // TouchPhase.Ended
            Drop();
            return;
        }                                                                                                                        



        if (Input.GetMouseButton(0)) // First we are gonna look for a mouse click
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0) // and then look for a single touch on the screen
        {
            _screenPosition = Input.GetTouch(0).position; // if none of those is detected
        }
        else
        {
            return; // we can return as there is nothing to drag
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition); // Convert the 2D touched position on the screen to world coordinates

        if (_isDragActive) // if we are already in dragmode we call drag
        {
            Drag();
        }
        else // else we have to detect the item we want to move and we can begin our drag
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero); // to let the script know if we clicked or touched on a draggable item on the screen, we can cast a 2D Ray on the world
                                                                                // position with Vector2.zero as direction. It is looking what is on the screen at the given position
            if(hit.collider != null) // If the Ray collides with an item and that item has our draggable component, we can store it on lastDragged and call InitDrag
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        UpdateDragStatus(true);
    }

   void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop()
    {
        _isDragActive = false;
    }

    void UpdateDragStatus(bool IsDragging)
    {
        _isDragActive = _lastDragged.IsDragging = IsDragging;
        gameObject.layer = IsDragging ? Layer.Dragging : Layer.Default;
    }
}
