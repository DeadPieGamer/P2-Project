using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_PointChecker : MonoBehaviour
{
    public int points;
    public GameObject wonMenu;

    public GameObject exit;
    public GameObject dictionary;

    // Start is called before the first frame update
    void Start()
    {
        wonMenu.SetActive(false);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= 4)
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
    }

}
