using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_inputHandler : MonoBehaviour
{
    private InputManager inputManager;


    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
        inputManager.OnStartTouch += LaserBeam;


    }
    private void LaserBeam(Vector2 postition, float time)
    {
        postition = Camera.main.ScreenToWorldPoint(postition);
        Debug.Log(postition);
        RaycastHit2D hit = Physics2D.Raycast(postition, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Draggable"))
            {
                hit.collider.GetComponent<KO_Draggable>().OnDrag(postition);
            }
        }
    }
}
