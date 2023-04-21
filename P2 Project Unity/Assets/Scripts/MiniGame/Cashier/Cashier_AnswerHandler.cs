using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Cashier_AnswerHandler : MonoBehaviour
{


    private WordCards answerCard;
    private SL_ListItem _listitem;
    public TextMeshProUGUI answer;
    
    private AudioSource answerAudio;
    
    public TextMeshProUGUI answerPlace;
    private SetTypes answerST;
    public void answerClick()
    {
        
        _listitem = GetComponentInParent<SL_ListItem>();
        answerCard = _listitem.itemCard;
        answerAudio = GetComponent<AudioSource>();
        answer.text = answerCard.danish_Word;
        answerST = _listitem.setType;
        answerAudio.PlayOneShot(answerCard.word_Audio);

        DefinePlace(answerST);

    }
    private void DefinePlace(SetTypes deck)
    {
        string savedData;
        switch (deck)
        {
            case SetTypes.Meat:
                answerPlace.text = "Kød";
                savedData = "Kød" ;
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;

            case SetTypes.Dairy:
                answerPlace.text = "Mejeri";
                savedData = "Mejeri";
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;

            case SetTypes.FruitsAndGreens:
                answerPlace.text = "FrugtogGrøn";
                savedData = "FrugtogGrøn";
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;
        }
    }
    
}
