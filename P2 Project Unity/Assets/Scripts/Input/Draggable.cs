using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]private InputManager inputManager;

    private void Start()
    {
        inputManager.OnStartTouch += Drag;
    }
    private void Drag(Vector2 postition, float time)
    {
        Debug.Log("Pressed at" + postition);
    }
}
