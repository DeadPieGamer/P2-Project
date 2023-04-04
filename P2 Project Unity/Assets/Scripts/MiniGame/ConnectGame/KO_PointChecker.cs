using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO_PointChecker : MonoBehaviour
{
    public int points;
    public GameObject wonMenu;
    // Start is called before the first frame update
    void Start()
    {
        wonMenu.SetActive(false);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        if (points >= 3)
        {
            Debug.Log("You won");
            wonMenu.SetActive(true);
            points = 0;
        }
    }
}
