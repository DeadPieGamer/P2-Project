using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KO_PointChecker : MonoBehaviour
{
    public SceneManger sceneManger;

    public int points;
    public GameObject wonMenu;

    public GameObject exit;
    public GameObject dictionary;

    public bool firstComplete = true;
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
            string boolData = File.ReadAllText(Application.persistentDataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
            if (boolData == "True,True,True,True,True,True" && PlayerPrefs.GetInt("firstPass", 0) != 1)
            {
                sceneManger.LoadScene("FinishShopList");
                PlayerPrefs.SetInt("firstPass", 1);
            }
        }
    }
    public void DisableButton()
    {
        exit.gameObject.SetActive(false);
        dictionary.gameObject.SetActive(false);
    }

}
