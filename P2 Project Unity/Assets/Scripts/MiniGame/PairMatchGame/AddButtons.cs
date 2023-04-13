using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddButtons : MonoBehaviour 
{
    [SerializeField]
    private Transform _puzzleField; 

    [SerializeField]
    private GameObject _btn;


    private void Awake()
    {
        for (int i = 0; i < 8; i++) // here 8 is the number of buttons/ Puzzlepieces  
        {
            GameObject button = Instantiate(_btn);//Creating a copy and asigning it to the gameobject
            button.name = "" + i;
            button.transform.SetParent(_puzzleField,false);
        }
    }
}
//Credit to Awesome Tuts
//https://www.youtube.com/@awesometuts