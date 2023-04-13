using System.Collections;
using System.Collections.Generic;
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

    public AudioSource _audio;
    public TextMeshProUGUI _wordText;

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


    private void Awake()
    {
        _puzzles = Resources.LoadAll<WordCards>("WordCards_Folder/FruitsAndGreens/");
    }

    private void Start()
    {
        GetButton();
        AddListerners();
        AddGamePuzzles();
        ShuffflePlacement(_gamePuzzles);
        gameGuesses = _gamePuzzles.Count / 2;
        _wordText = GetComponentInChildren<TextMeshProUGUI>();


    }
    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = _bgImage; //adding the image to our cards
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

        }
    }
    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //convert a string to an int
            firstGuessPuzzle = _gamePuzzles[firstGuessIndex].danish_Word;  //getting the name of the image
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
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //convert a string to an int
            secondGuessPuzzle = _gamePuzzles[secondGuessIndex].danish_Word;  //getting the name of the image and comparing them 
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
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
//Credit to Awesome Tuts
//https://www.youtube.com/@awesometuts
