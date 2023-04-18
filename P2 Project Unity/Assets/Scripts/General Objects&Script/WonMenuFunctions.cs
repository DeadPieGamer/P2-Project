using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonMenuFunctions : MonoBehaviour
{
    /// <summary>
    /// Loads the currently active scene
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the main game scene (Super Market)
    /// </summary>
    public void LoadMainScene()
    {
        SceneManager.LoadScene(2);
    }
}
