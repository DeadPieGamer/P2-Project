using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class D_Grabber : MonoBehaviour
{
    public WordCards refcard;
    [SerializeField] private GameObject refObject;
    
    void Start()
    {
        refcard = refObject.GetComponent<Sorting_Game>().AssignedCard;
        Top(refcard);
    }
    
    private void Top(WordCards card)
    {
        Debug.Log(card.danish_Word);
    }
}
