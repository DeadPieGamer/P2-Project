using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector3 StartPosition = new Vector3(-400.15f, 600.62f, 0);

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

    private Vector3 _offset = new Vector3(150f, 152f, 0);
    private Vector3 _offsetFor15pairs = new Vector3(105f, 120f, 0);
    private Vector3 _offsetFor20pairs = new Vector3(105f, 100f, 0);

    private Vector3 _newScaleDown = new Vector3(90f, 90f, 0.001f);

    private List<Material> _materialList = new List<Material>();
    private List<string> _texturePathList = new List<string>();

    private Material _firstMaterial;
    private string _firstTexturePath;

    private int _firstReaveledPic;
    private int _secondReaveledPic;
    private int _reaveledPicNumber = 0;
    private int _picTodestory1;
    private int _picTodestory2;


    void Start()
    {
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
        PuzzleRevealedNumber = RevealedState.NoRevealed;
        _reaveledPicNumber = 0;
        _firstReaveledPic = -1;
        _secondReaveledPic = -1;

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
        System.Threading.Thread.Sleep(200);
        PictureList[_picTodestory1].Deactivate();
        PictureList[_picTodestory2].Deactivate();
        _reaveledPicNumber = 0;
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
    }

    private void FlipBack()
    {
        PictureList[_firstReaveledPic].FlipBack();
        PictureList[_secondReaveledPic].FlipBack();

        PictureList[_firstReaveledPic].Revealed = false;
        PictureList[_secondReaveledPic].Revealed = false;

        PuzzleRevealedNumber = RevealedState.NoRevealed;
        CurrentGameState = GameState.NoAction;
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
            }
        }
        if (CurrentGameState == GameState.FlipBack)
        {
            if (CurrentPuzzleState == PuzzleState.CanRotate)
            {
                FlipBack();
            }
        }
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
        var randomDis = 140;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDis * Time.deltaTime);
            yield return 0;
        }
    }
          
}