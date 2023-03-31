using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public WordCards myItem;
    public Vector2 startPos;


    private void Start()
    {
        startPos= transform.position;
    }
    public void moveBack()
    {
        transform.position= startPos;
    }
}
