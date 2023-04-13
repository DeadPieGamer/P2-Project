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
        
        string savedData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/PlaceDatafile.txt").ToString();
        switch (savedData)
        {
            case "K�d":
                outline = meatButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                //highLight(boolMEAT);
                //outline.ChangeOutline(boolMEAT);
            break;
            case "FrughtogGr�nt":
                outline = meatButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                //highLight(boolVEGG);
                //outline.ChangeOutline(boolVEGG);
            break;
            case "Mejeri":
                outline = meatButton.GetComponent<DummyOutlineHandler>();
                outline.isOutlined = true;
                //highLight(boolDAIRY);
                //outline.ChangeOutline(boolDAIRY);
            break;
            default: 
                
            break;
        }
    }

    public bool highLight(bool place)
    {
        return place = true;
    }
}
