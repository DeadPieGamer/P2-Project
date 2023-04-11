using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite _bgImage;
    public List<Button> btns = new List<Button>();

    public Sprite[] _puzzles;
    public List<Sprite> _gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private void Awake()
    {
        _puzzles = Resources.LoadAll<Sprite>("Graphics/PuzzleCat/");//might have to add the last folder 
    }

    private void Start()
    {
        GetButton();
        AddListerners();
        AddGamePuzzles();
        Shufffle(_gamePuzzles);
        gameGuesses = _gamePuzzles.Count / 2;
    }
    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = _bgImage; //adding the image to our cards
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
            firstGuessPuzzle = _gamePuzzles[firstGuessIndex].name;  //getting the name of the image 
            btns[firstGuessIndex].image.sprite = _gamePuzzles[firstGuessIndex];
        }

        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //convert a string to an int
            secondGuessPuzzle = _gamePuzzles[secondGuessIndex].name;  //getting the name of the image and comparing them 
            btns[secondGuessIndex].image.sprite = _gamePuzzles[secondGuessIndex];
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());

        }
        
    }
    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;//can not click on the button after choosen the right pair 
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);//can not see the button after choosen the right pair 
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].image.sprite = _bgImage;
            btns[secondGuessIndex].image.sprite = _bgImage;
        }
        yield return new WaitForSeconds(0.5f);
        firstGuess = secondGuess = false;
    }
    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("it took you" + countGuesses + "Guess(es) to finish the game");

        }
    }
    void Shufffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
//Credit to Awesome Tuts - Anyone Can Learn To Make Games For Inspo
//https://www.youtube.com/@awesometuts
