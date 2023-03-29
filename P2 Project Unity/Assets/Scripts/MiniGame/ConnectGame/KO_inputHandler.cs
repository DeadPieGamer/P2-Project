using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class KO_inputHandler : MonoBehaviour
{
    private InputManager inputManager;
    private GameObject SelectedObject;
    private AudioSource ObjectAudio;

    [SerializeField] private LayerMask layersToHit;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
        inputManager.OnStartTouch += LaserBeam;
        inputManager.OnEndTouch += Lift;
        
    }
    private void LaserBeam(Vector2 InputPosition, float time)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(InputPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layersToHit);
        if (hit.collider != null)
        {
            
            if (hit.collider.CompareTag("Draggable"))
            {
               
                Debug.Log(hit.collider.gameObject.name);
                SelectedObject = hit.collider.gameObject;
                inputManager.OnContinuedTouch += Dodrag;
                Play_Audio();

            }
        }
    }
    private void Lift(Vector2 Pos,float time)
    {
        inputManager.OnContinuedTouch -= Dodrag;
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<KO_Draggable>().CollidingDetect();
            SelectedObject = null;
        }
    }
    private void Play_Audio()
    {
       SelectedObject.GetComponent<AudioSource>().Play();
    }

    private void Dodrag(Vector2 inputPosition)
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y));
        SelectedObject.transform.position = Pos;
    }

}
