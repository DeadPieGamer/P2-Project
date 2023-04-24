using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OutlineHandler : MonoBehaviour
{
    public bool boolMEAT = false;
    public bool boolVEGG = false;
    public bool boolDAIRY = false;

    public Button meatButton;
    public Button veggButton;
    public Button dairyButton;

    public DummyOutlineHandler outline;
    // Start is called before the first frame update
    void Awake()
    {
        
        string savedData = File.ReadAllText(Application.persistentDataPath + "/Resources/ShopListData/PlaceDatafile.txt").ToString();
        switch (savedData)
        {
            case "Kød":
                outline = meatButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                Debug.Log("M");
                StartCoroutine(blinkButton(meatButton));
            break;
            case "FrugtogGrøn":
                outline = veggButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                Debug.Log("V");
                StartCoroutine(blinkButton(veggButton));

                break;
            case "Mejeri":
                outline = dairyButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                Debug.Log("D");
                StartCoroutine(blinkButton(dairyButton));

                break;
            default: 
                
            break;
        }
    }

    public bool highLight(bool place)
    {
        return place = true;
    }
    public void removePlaceFile()
    {
        string savedData = "";
        File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
    }
    public IEnumerator blinkButton(Button button)
    {
        button.image.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        button.image.color = Color.gray;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(blinkButton(button));
    }
}