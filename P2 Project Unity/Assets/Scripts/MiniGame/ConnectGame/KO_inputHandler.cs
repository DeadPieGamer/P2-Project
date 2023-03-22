using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_inputHandler : MonoBehaviour
{
    private InputManager inputManager;
    private GameObject SelectedObject;
    private bool isDragging;
    private AudioSource ObjectAudio;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
        inputManager.OnStartTouch += LaserBeam;
        inputManager.OnEndTouch += Lift;
        isDragging = false;
    }
    private void LaserBeam(Vector2 postition, float time)
    {
        Vector3 Pos = Camera.main.ScreenToWorldPoint(new Vector3(postition.x,postition.y,10f));
        Ray ray = Camera.main.ScreenPointToRay(postition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            
            if (hit.collider.CompareTag("Draggable"))
            {
                //hit.collider.GetComponent<KO_Draggable>().OnDrag(postition);
                Debug.Log(hit.collider.gameObject.name);
                SelectedObject = hit.collider.gameObject;
                isDragging=true;
                
            }
            if (isDragging)
            {
                
                SelectedObject.transform.position = Pos;
                Debug.Log("Dragging");
            }
        }
    }
    private void Lift(Vector2 Pos,float time)
    {
        isDragging = false;
        Debug.Log("Stop Dragging");
    }
    private void Play_Audio()
    {
       
    }



}
