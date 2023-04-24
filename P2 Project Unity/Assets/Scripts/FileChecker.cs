using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileChecker : MonoBehaviour
{
    string boolPath = "/Resources/ShopListData/boolDatafile.txt";
    string shopListPath = "/Resources/ShopListData/cardDatafile.txt";
    string placePath = "/Resources/ShopListData/PlaceDatafile.txt";

    string mainPath = "/Resources/ShopListData";
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        var persist = Application.persistentDataPath;
        Directory.CreateDirectory(persist + mainPath);
        if (!File.Exists(persist + boolPath))
        {
            File.Create(persist + boolPath);
        } 
       if(!File.Exists(persist + shopListPath))
        {
            File.Create (persist + shopListPath);
        }
       if(!File.Exists(persist + placePath))
        {
            File.Create(persist + placePath);
        }
        
    }

}
