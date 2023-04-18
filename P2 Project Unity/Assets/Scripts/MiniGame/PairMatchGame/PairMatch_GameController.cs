using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PairMatch_GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite _bgImage;
    public List<Button> btns = new List<Button>();

    public WordCards[] _puzzles;
    public List<WordCards> _gamePuzzles = new List<WordCards>();
    public WordCards cardCheck;

    public AudioSource _audio;
    private TextMeshProUGUI _wordText;

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;
    public GameObject EndGamePanel;

    [SerializeField] private AudioSource _anwsersSoundSource;
    [SerializeField] private AudioClip correctDing;
    [SerializeField] private AudioClip wrongDing;

    [SerializeField] private CardSetLoader loader;
    private SetTypes gameSet = SetTypes.FruitsAndGreens;

    int startIndex = 2;
    List<int> wholeArrayInd;
    List<int> ArrayNum = new List<int>();
    List<WordCards> cardSlot = new List<WordCards>();
    private List<bool> LearnedArray = new List<bool>();

    private void Awake()
    {
        _puzzles = Resources.LoadAll<WordCards>("WordCards_Folder/FruitsAndGreens/");
       

    }

    private void Start()
    {
        int Setamount = 6;

        loader = GameObject.FindGameObjectWithTag("loader").GetComponent<CardSetLoader>();

        GetButton();
        AddListerners();
        AddGamePuzzles();
        ShuffflePlacement(_gamePuzzles);
        gameGuesses = _gamePuzzles.Count / 2;
        _wordText = GetComponentInChildren<TextMeshProUGUI>();

        string wordData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
        wholeArrayInd = wordData.Split(',').ToList().Select(int.Parse).ToList();

        for (int i = startIndex; i < startIndex + 2; i++)
        {
            ArrayNum.Add(wholeArrayInd[i]);
        }

        for (int i = startIndex - startIndex; i < startIndex; i++)
        {
            cardSlot.Add(loader.Get_Set(gameSet)[ArrayNum[i]]);
            Debug.Log(cardSlot[i].danish_Word);
        }

        string boolData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
        string[] convertstep = boolData.Split(',').ToArray();
        for (int i = 0; i < Setamount; i++)
        {
            LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
        }
    }
    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = _bgImage; //adding the back image to our cards

            // Goes through every text the button may have and resets the text
            foreach (TextMeshProUGUI textBox in btns[i].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = "";
            }
        }
    }
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            _gamePuzzles.Add(_puzzles[index]);
            index++;
        }
    }

    void AddListerners() //Adding the fuction to our buttons 
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
            ShufffleImage(_puzzles);
        }
    }
    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;
            //convert a string to an int
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            //getting the name of the image
            firstGuessPuzzle = _gamePuzzles[firstGuessIndex].danish_Word;  
            btns[firstGuessIndex].image.sprite = _gamePuzzles[firstGuessIndex].word_Picture;
            // Because image of the button was changed, we change the text to match
            foreach (TextMeshProUGUI textBox in btns[firstGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = _gamePuzzles[firstGuessIndex].danish_Word;
            }
            //Getting the audio attach to the wordcards 
            _audio.clip = _gamePuzzles[firstGuessIndex].word_Audio;
            _audio.Play();

        }

        else if (!secondGuess)
        {
            secondGuess = true;
            //convert a string to an int
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            //getting the name of the image and comparing them 
            secondGuessPuzzle = _gamePuzzles[secondGuessIndex].danish_Word;  
            btns[secondGuessIndex].image.sprite = _gamePuzzles[secondGuessIndex].word_Picture;
            // Because image of the button was changed, we change the text to match
            foreach (TextMeshProUGUI textBox in btns[secondGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = _gamePuzzles[secondGuessIndex].danish_Word;
            }
            //Getting the audio attach to the wordcards 
            _audio.clip = _gamePuzzles[secondGuessIndex].word_Audio;
            _audio.Play();
            //Adding to the score 
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());
        }
        
    }
    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            
            yield return new WaitForSeconds(0.2f);
            btns[firstGuessIndex].interactable = false;//can not click on the button after choosen the right pair 
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);//can not see the button after choosen the right pair 
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            // Because images were removed, we also empty the text fields
            foreach (TextMeshProUGUI textBox in btns[firstGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = "";
            }
            foreach (TextMeshProUGUI textBox in btns[secondGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = "";
            }

            //Debug.Log(cardCheck.danish_Word);
            CheckShoplist(_gamePuzzles[secondGuessIndex].danish_Word);
            _anwsersSoundSource.PlayOneShot(correctDing);
            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].image.sprite = _bgImage;
            btns[secondGuessIndex].image.sprite = _bgImage;
            // Because images were reset, we also reset the words
            foreach (TextMeshProUGUI textBox in btns[firstGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = "";
            }
            foreach (TextMeshProUGUI textBox in btns[secondGuessIndex].GetComponentsInChildren<TextMeshProUGUI>())
            {
                textBox.text = "";
            }
            _anwsersSoundSource.PlayOneShot(wrongDing);

        }
        yield return new WaitForSeconds(0.2f);
        firstGuess = secondGuess = false;
    }
    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {
            EndGamePanel.SetActive(true);
            Debug.Log("Game Finished");
            Debug.Log("it took you " + countGuesses + " Guess(es) to finish the game");

        }
    }
    void ShuffflePlacement(List<WordCards> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            WordCards temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    void ShufffleImage(WordCards[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            WordCards temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Length);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private void CheckShoplist(string card)
    {
        for (int i = startIndex; i < startIndex + 2; i++)
        {
            if (cardSlot[i - startIndex].danish_Word == card)
            {
                LearnedArray[i] = true;
                string boolData = String.Join(",", LearnedArray);
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt", boolData);
            }
            
        }
    }
}
//Credit to Awesome Tuts
//https://www.youtube.com/@awesometuts
