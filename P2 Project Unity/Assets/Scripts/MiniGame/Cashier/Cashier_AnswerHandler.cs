using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Cashier_AnswerHandler : MonoBehaviour
{
    [SerializeField] private AudioClip whereAud;
    [SerializeField] private AudioClip inAud;
    [SerializeField] private AudioClip placeAud;

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
        answerAudio = GameObject.FindGameObjectWithTag("checker").GetComponent<AudioSource>();
        answer.text = answerCard.danish_Word;
        answerST = _listitem.setType;

        DefinePlace(answerST);
        StartCoroutine(playPlaceAnswer_audio());
    }
    private void DefinePlace(SetTypes deck)
    {
        string savedData;
        switch (deck)
        {
            case SetTypes.Meat:
                answerPlace.text = "K�d";
                savedData = "K�d" ;
                File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;

            case SetTypes.Dairy:
                answerPlace.text = "Mejeri";
                savedData = "Mejeri";
                File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;

            case SetTypes.FruitsAndGreens:
                answerPlace.text = "FrugtogGr�n";
                savedData = "FrugtogGr�n";
                File.WriteAllText(Application.persistentDataPath + "/Resources/ShopListData/PlaceDatafile.txt", savedData);
                break;
        }
    }
    
    IEnumerator playPlaceAnswer_audio()
    {
        answerAudio.clip = answerCard.word_Audio;
        answerAudio.Play();
        yield return new WaitForSeconds(answerAudio.clip.length);
    }
}
