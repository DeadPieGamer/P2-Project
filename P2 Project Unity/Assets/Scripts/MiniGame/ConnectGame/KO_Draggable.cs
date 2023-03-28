using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KO_Draggable : MonoBehaviour
{
    public WordCards myCard;
    private GameObject hitObject;
    private BoxCollider2D coll;

    private void Start()
    {
        myCard = GetComponent<Connect_Game>().AssignedCard;
        coll = GetComponent<BoxCollider2D>();
    }

    public void CollidingDetect()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,0f , Vector2.zero);
        if (hit.collider != null)
        {
            hitObject = hit.collider.gameObject;
            if(hitObject.GetComponent<Connect_Game>().AssignedCard == myCard)
            {
                Correct();
            }
            else
            {
                inCorrect();
            }
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

    
}
