using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_Draggable : MonoBehaviour
{
    private InputManager inputManager;
    

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
        inputManager.OnStartTouch += Drag;


    }
    private void Drag(Vector2 postition, float time)
    {
        Debug.Log("Pressed at" + postition);
        
    }

}
