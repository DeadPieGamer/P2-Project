using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private readonly Dictionary<EPuzzleCategories, string> _puzzleCatDirectory = new Dictionary<EPuzzleCategories, string>();//Storing the category name and folder 
    private int _settings;
    private const int SettingsNumber = 2; //set 1 setting in order to start the game 
    private bool _muteFXPermanently = false;

    public enum EPairNumber
    {
        NotSet = 0,
        E6Pairs = 10,
        E8Pairs = 15,
        E10Pairs = 20,
    }

    public enum EPuzzleCategories
    {
        NotSet,
        All,
    }


    public struct Settings
    {
        public EPairNumber PairsNumber;
        public EPuzzleCategories PuzzleCategory;
    }

    private Settings _gameSettings;

    public static GameSettings Instance; //to make sure the script is not detroyed on when changing scenes  

    void Awake()
    {
        if (Instance == null) 
        {
            DontDestroyOnLoad(target: this);
            Instance = this;
        }

        else
        {
            Destroy(obj: this);
        }
    }
    void Start()
    {
        SetPuzzleCatDirectory();
        _gameSettings = new Settings();
        ResetGameSettings();
    }
    private void SetPuzzleCatDirectory()
    {
        _puzzleCatDirectory.Add(EPuzzleCategories.All, "All");
    }

    public void SetPairNumber(EPairNumber Number)
    {
        if (_gameSettings.PairsNumber == EPairNumber.NotSet)
            _settings++;
        _gameSettings.PairsNumber = Number;
    }
    public void SetPuzzleCategories(EPuzzleCategories cat)
    {
        if (_gameSettings.PuzzleCategory == EPuzzleCategories.NotSet)
            _settings++;
        _gameSettings.PuzzleCategory = cat;
    }

    public EPairNumber GetPairNumber()
    {
        return _gameSettings.PairsNumber;
    }

    public EPuzzleCategories GetPuzzleCaategory()
    {
        return _gameSettings.PuzzleCategory;
    }
    public void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.PuzzleCategory = EPuzzleCategories.NotSet;
        _gameSettings.PairsNumber = EPairNumber.NotSet;
    }
    public bool AllSettingsReady()
    {
        return _settings == SettingsNumber;
    }
    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }
    public string GetPuzzleCategoryTextureDirectoryName()
    {
        if (_puzzleCatDirectory.ContainsKey(_gameSettings.PuzzleCategory))
        {
            return "Graphics/PuzzleCat/" + _puzzleCatDirectory[_gameSettings.PuzzleCategory] + "/";
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY NAME");
            return "";
        }
    }
    public void MuteSoundEffectPermanently(bool muted)
    {
        _muteFXPermanently = muted;
    }

    public bool IsSoundMutedPermanently()
    {
        return _muteFXPermanently;
    }

}


