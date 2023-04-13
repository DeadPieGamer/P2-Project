using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OutlineHandler : MonoBehaviour
{
    [SerializeField] private Button Meat;
    [SerializeField] private Button Vegg;
    [SerializeField] private Button Dairy;
    // Start is called before the first frame update
    void Start()
    {
        string savedData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/PlaceDatafile.txt").ToString();
        switch (savedData)
        {
            case "Kød":
                highLight("Meat"); 
            break;
            case "FrughtogGrønt":
                highLight("Vegg");
            break;
            case "Mejeri":
                highLight("Dairy");
            break;
            default: 
                highLight("Nothing");
            break;
        }
    }

    private void highLight(string Place)
    {
        Debug.Log("Highlight" + Place);
    }
}
