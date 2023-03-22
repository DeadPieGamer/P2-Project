using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

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
                inputManager.OnContinuedTouch += Dodrag;

            }
        }
    }
    private void Lift(Vector2 Pos,float time)
    {
     
        inputManager.OnContinuedTouch -= Dodrag;
    }
    private void Play_Audio()
    {
       
    }

    private void Dodrag(Vector2 pos)
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y));
        SelectedObject.transform.position = Pos;
    }


}
