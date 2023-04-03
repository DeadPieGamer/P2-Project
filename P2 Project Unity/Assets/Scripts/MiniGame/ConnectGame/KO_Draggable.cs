using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KO_Draggable : MonoBehaviour
{
    public WordCards myCard;
    private GameObject hitObject;
    private BoxCollider2D coll;
    private Vector3 PlusSize = new Vector3(0f, 0f,0f);

    [SerializeField] private LayerMask layersToHit;

    //[SerializeField] private bool drawStuff = false;

    private void Start()
    {
        myCard = GetComponent<Connect_Game>().AssignedCard;
        coll = GetComponent<BoxCollider2D>();

    }

    public void CollidingDetect()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size + PlusSize,0f , Vector2.zero, Mathf.Infinity, layersToHit);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("DanishWord"))
            {
                hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Connect_Game>().AssignedCard == myCard)
                {
                    Correct();
                }
                else
                {
                    inCorrect();
                }
            }
            else
            {
                Debug.Log("Didn't hit text");
            }
            Debug.Log("I hit " + hit.collider.name);
        }
    }

    private void Correct()
    {

    Debug.Log("Correct");
        

    }
    private void inCorrect()
    {

        Debug.Log("inCorrect");

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    if (drawStuff)
    //        Gizmos.DrawWireCube(coll.bounds.center, coll.bounds.size + PlusSize);

    //}


}
