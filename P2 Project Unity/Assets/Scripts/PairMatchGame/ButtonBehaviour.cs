using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{

    private void Start()
    {
        Config.CreateScoreFile();
    }

    public void LoadScene(string scene_name) //loads scene when called 
    {
        SceneManager.LoadScene(scene_name);
    }

    public void ResetGameSettings()
    {
        GameSettings.Instance.ResetGameSettings();
    }
}

