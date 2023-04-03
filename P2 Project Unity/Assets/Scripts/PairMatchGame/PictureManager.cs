using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector3 StartPosition = new Vector3(-400f, 700f, 0);

    [Space]
    [Header("End Game Screen")]
    public GameObject EndGamePanel;
    public GameObject NewBestScoreText;
    public GameObject YourScoreText;
    public GameObject EndTimeText;

    public enum GameState
    { NoAction, MovingOnPositions, DeletingPuzzles, FlipBack, Checking, GameEnd };


    public enum PuzzleState
    { PuzzleRotating, CanRotate };

    public enum RevealedState
    { NoRevealed, OneRevealed, TwoRevealed };

    [HideInInspector]
    public GameState CurrentGameState;
    [HideInInspector]
    public PuzzleState CurrentPuzzleState;
    [HideInInspector]
    public RevealedState PuzzleRevealedNumber;

    [HideInInspector]
    public List<Picture> PictureList;

    private Vector3 _offset = new Vector3(205f, 250f, 0);
    private Vector3 _offsetFor15pairs = new Vector3(205f, 220f, 0);
    private Vector3 _offsetFor20pairs = new Vector3(205f, 180f, 0);

    private Vector3 _newScaleDown = new Vector3(200f, 200f, 0.001f);

    private List<Material> _materialList = new List<Material>();
    private List<string> _texturePathList = new List<string>();

    private Material _firstMaterial;
    private string _firstTexturePath;

    private int _firstReaveledPic;
    private int _secondReaveledPic;
    private int _reaveledPicNumber = 0;
    private int _picTodestory1;
    private int _picTodestory2;

    private bool _corutineStarted = false;

    private int _pairNumbers;
    private int _removePairs;

    private Timer _gameTimer;

    void Start()
    {
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
        PuzzleRevealedNumber = RevealedState.NoRevealed;
        _reaveledPicNumber = 0;
        _firstReaveledPic = -1;
        _secondReaveledPic = -1;
        _removePairs = 0;
        _pairNumbers = (int)GameSettings.Instance.GetPairNumber();
        _gameTimer = GameObject.Find("Main Camera").GetComponent<Timer>();

        LoadMaterials();
        if (GameSettings.Instance.GetPairNumber() == GameSettings.EPairNumber.E10Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpwanPictureMesh(4, 5, StartPosition, _offset, false);
            MovePicture(4, 5, StartPosition, _offset);
        }
        else if (GameSettings.Instance.GetPairNumber() == GameSettings.EPairNumber.E15Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpwanPictureMesh(5, 6, StartPosition, _offset, false);
            MovePicture(5, 6, StartPosition, _offsetFor15pairs);
        }
        else if (GameSettings.Instance.GetPairNumber() == GameSettings.EPairNumber.E20Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpwanPictureMesh(5, 8, StartPosition, _offset, true);
            MovePicture(5, 8, StartPosition, _offsetFor20pairs);
        }
    }
    public void CheckPicture() //check how many pictures revealed 
    {
        CurrentGameState = GameState.Checking;
        _reaveledPicNumber = 0;

        for (int id = 0; id < PictureList.Count; id++)
        {
            if (PictureList[id].Revealed && _reaveledPicNumber < 2)
            {
                if (_reaveledPicNumber == 0)
                {
                    _firstReaveledPic = id;
                    _reaveledPicNumber++;
                }
                else if (_reaveledPicNumber == 1)
                {
                    _secondReaveledPic = id;
                    _reaveledPicNumber++;
                }
            }
        }

        if(_reaveledPicNumber==2) //revealed 2 picture then flip them back 
        {
            if (PictureList[_firstReaveledPic].GetIndex() == PictureList[_secondReaveledPic].GetIndex() && _firstReaveledPic != _secondReaveledPic)
            {
                CurrentGameState = GameState.DeletingPuzzles;
                _picTodestory1 = _firstReaveledPic;
                _picTodestory2 = _secondReaveledPic;
            }
            else
            {
                CurrentGameState = GameState.FlipBack;
            }

        }
        CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;

        if (CurrentGameState == GameState.Checking)
        {
            CurrentGameState = GameState.NoAction;
        }
    }

    private void DestroyPicture()
    {
        PuzzleRevealedNumber = RevealedState.NoRevealed;
        PictureList[_picTodestory1].Deactivate();
        PictureList[_picTodestory2].Deactivate();
        _reaveledPicNumber = 0;
        _removePairs++;
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
    }

    private IEnumerator FlipBack()
    {
        _corutineStarted = true;

        yield return new WaitForSeconds(0.5f);
        PictureList[_firstReaveledPic].FlipBack();
        PictureList[_secondReaveledPic].FlipBack();

        PictureList[_firstReaveledPic].Revealed = false;
        PictureList[_secondReaveledPic].Revealed = false;

        PuzzleRevealedNumber = RevealedState.NoRevealed;
        CurrentGameState = GameState.NoAction;
        _corutineStarted = false;
    }
    private void LoadMaterials()
    {
        var materialFilePath = GameSettings.Instance.GetMaterialDirectoryName();
        var textureFilePath = GameSettings.Instance.GetPuzzleCategoryTextureDirectoryName();
        var pairNumber = (int)GameSettings.Instance.GetPairNumber();
        const string matBaseName = "Pic";
        var firstMaterialName = "Back";

        for (var index = 1; index <= pairNumber; index++)
        {
            var currentFilePath = materialFilePath + matBaseName + index;
            Material mat = Resources.Load(currentFilePath, typeof(Material)) as Material;
            _materialList.Add(mat);
            var currenTextureFilePath = textureFilePath + matBaseName + index;
            _texturePathList.Add(currenTextureFilePath);
        }

        _firstTexturePath = textureFilePath + firstMaterialName;
        _firstMaterial = Resources.Load(materialFilePath + firstMaterialName, typeof(Material)) as Material;
    }
    void Update()
    {
        if (CurrentGameState == GameState.DeletingPuzzles)
        {
            if (CurrentPuzzleState == PuzzleState.CanRotate)
            {
                DestroyPicture();
                CheckGameEnd();
            }
        }
        if (CurrentGameState == GameState.FlipBack)
        {
            if (CurrentPuzzleState == PuzzleState.CanRotate && _corutineStarted ==false)
            {
                StartCoroutine(FlipBack());
            }
        }
        if (CurrentGameState == GameState.GameEnd)
        {
            if (PictureList[_firstReaveledPic].gameObject.activeSelf == false
                && PictureList[_secondReaveledPic].gameObject.activeSelf == false
                && EndGamePanel.activeSelf == false)
            {
                ShowEndGameInformation();
            }
        }
    }

    private bool CheckGameEnd()
    {
        if (_removePairs == _pairNumbers && CurrentGameState != GameState.GameEnd)
        {
            CurrentGameState = GameState.GameEnd;
            _gameTimer.StopTimer();
            Config.PlaceScoreOnBoard(_gameTimer.GetCurrentTime());
        }
        return (CurrentGameState == GameState.GameEnd);
    }

    private void ShowEndGameInformation()
    {
        EndGamePanel.SetActive(true);
        if (Config.IsBestScore())
        {
            NewBestScoreText.SetActive(true);
            YourScoreText.SetActive(false);
        }
        else
        {
            NewBestScoreText.SetActive(false);
            YourScoreText.SetActive(true);
        }

        var timer = _gameTimer.GetCurrentTime();
        var minuets = Mathf.Floor(timer / 60);
        var seconds = Mathf.RoundToInt(timer % 60);
        var newText = minuets.ToString("00") + ":" + seconds.ToString("00");
        EndTimeText.GetComponent<Text>().text = newText;
    }

    private void SpwanPictureMesh(int rows, int columns, Vector3 Pos, Vector3 offset, bool scaleDown)
    {
        for (int col = 0; col < columns; col++)
            for (int row = 0; row < rows; row++)
            {
                var tempPicture = (Picture)Instantiate(PicturePrefab, PicSpawnPosition.position, PicturePrefab.transform.rotation);
                if (scaleDown)
                {
                    tempPicture.transform.localScale = _newScaleDown;
                }
                tempPicture.name = tempPicture.name + 'c' + col + 'r' + row;
                PictureList.Add(tempPicture);
            }
        ApplyTextures();
            
    }
    public void ApplyTextures()
    {
        var rndMatIndex = Random.Range(0, _materialList.Count);
        var AppliedTimes = new int[_materialList.Count];

        for (int i = 0; i < _materialList.Count; i++)
        {
            AppliedTimes[i] = 0; 
        }
        foreach (var o in PictureList)
        {
            var randPrevious = rndMatIndex;
            var counter = 0;
            var forceMat = false;

            while (AppliedTimes[rndMatIndex] >= 2 || ((randPrevious == rndMatIndex) && !forceMat))
            {
                rndMatIndex = Random.Range(0, _materialList.Count);
                counter++;
                if (counter > 100) //tried more than 100 times, then look agian and apply the first material which have not been applied  
                {
                    for (var j = 0; j < _materialList.Count; j++)
                    {
                        if (AppliedTimes[j] < 2)
                        {
                            rndMatIndex = j;
                            forceMat = true; 
                        }
                    }

                    if (forceMat == false)
                        return;
                }
            }
            o.SetFirstMaterial(_firstMaterial, _firstTexturePath);
            o.ApplyFirstMaterial();
            o.SetSecondMaterial(_materialList[rndMatIndex],_texturePathList[rndMatIndex]);
            o.SetIndex(rndMatIndex);
            o.Revealed = false;
            AppliedTimes[rndMatIndex] += 1;
            forceMat = false;
        }
    }
    private void MovePicture(int rows, int columns, Vector3 pos, Vector3 offset)
    {
        var index = 0;
        for (var col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 20.0f);
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index]));
                index++;
            }
        }
    }
    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var randomDis = 400;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDis * Time.deltaTime);
            yield return 0;
        }
    }
          
}