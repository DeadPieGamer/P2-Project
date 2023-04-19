using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sorting_PointChecker : MonoBehaviour
{
    public int points;

    public GameObject exit;
    public GameObject dictionary;
    public GameObject settings;

    public GameObject wonMenu;
    // Start is called before the first frame update
    void Start()
    {
        wonMenu.SetActive(false);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= 6)
        {
            Debug.Log("You won");
            wonMenu.SetActive(true);
            points = 0;
            DisableButton();
        }
    }

    public void DisableButton()
    {
            exit.gameObject.SetActive(false);
            dictionary.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
    }
  
}
