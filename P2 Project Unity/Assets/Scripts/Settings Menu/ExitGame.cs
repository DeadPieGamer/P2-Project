using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Quitgame()
    {
        Application.Quit();
        Debug.Log("Game Quitted");
    }
}
