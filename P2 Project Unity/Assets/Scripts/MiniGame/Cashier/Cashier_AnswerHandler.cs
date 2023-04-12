using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cashier_AnswerHandler : MonoBehaviour
{


    private WordCards answerCard;
    private SL_ListItem _listitem;
    public TextMeshProUGUI answer;
    //public AudioSource[] wordaudiosource;

    // Start is called before the first frame update
    public TextMeshProUGUI answerPlace;
    private SetTypes answerST;
    public void answerClick()
    {
        //Debug.Log(transform.parent.name);
        _listitem = GetComponentInParent<SL_ListItem>();
        answerCard = _listitem.itemCard;
        answer.text = answerCard.danish_Word;
        answerST = _listitem.setType;
        //Debug.Log(answerST);

        DefinePlace(answerST);

    }
    private void DefinePlace(SetTypes deck)
    {
        switch (deck)
        {
            case SetTypes.Meat:
                answerPlace.text = "K�d";
            break;

            case SetTypes.Dairy:
                answerPlace.text = "Mejeri";
            break;

            case SetTypes.FruitsAndGreens:
                answerPlace.text = "Gr�n";
            break;
        }
    }
}
