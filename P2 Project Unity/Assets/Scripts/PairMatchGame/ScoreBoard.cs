using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ScoreBoard : MonoBehaviour
{
    public Text[] scoresText_6Pairs;
    public Text[] dateText_6Pairs;

    public Text[] scoresText_8Pairs;
    public Text[] dateText_8Pairs;

    public Text[] scoresText_10Pairs;
    public Text[] dateText_10Pairs;

    void Start()
    {
        UpdateScoreBoard();
    }

    public void UpdateScoreBoard()
    {
        Config.UpdateScoreList();
        DisplayPairsScoreData(Config.ScoreTimeList10Pairs, Config.PairNumberList6Pairs, scoresText_6Pairs, dateText_6Pairs);
        DisplayPairsScoreData(Config.ScoreTimeList10Pairs, Config.PairNumberList8Pairs, scoresText_8Pairs, dateText_8Pairs);
        DisplayPairsScoreData(Config.ScoreTimeList10Pairs, Config.PairNumberList10Pairs, scoresText_10Pairs, dateText_10Pairs);

    }

    private void DisplayPairsScoreData(float[] scoreTimeList, string[] pairNumberList, Text[] scoreText, Text[] dataText)
    {
        for (var index = 0; index < 3; index++)
        {
            if (scoreTimeList[index] > 0)
            {
                var deltaTime = Regex.Split(pairNumberList[index], "T");
                var minutes = Mathf.Floor(scoreTimeList[index] / 60);
                float seconds = Mathf.RoundToInt(scoreTimeList[index] % 60);

                scoreText[index].text = minutes.ToString("00") + ":" + seconds.ToString("00");
                if (dataText.Length >= 2)
                {
                    dataText[index].text = deltaTime[0] + "" + deltaTime[1];
                }
            }
            else 
            {
                scoreText[index].text = " ";
                dataText[index].text = "";                    
            }
     
        }
    }
}
//Credit to CodePlanStudio for the inspo
