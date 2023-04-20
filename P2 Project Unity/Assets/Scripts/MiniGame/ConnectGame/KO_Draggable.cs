using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class KO_Draggable : MonoBehaviour
{
    [SerializeField]private CardSetLoader loader;
    public WordCards myCard;
    private GameObject hitObject;
    private BoxCollider2D coll;
    private Vector3 PlusSize = new Vector3(-1f, -1f,0f);
    private Vector2 startPos;
    [SerializeField] private LayerMask layersToHit;
    private LineRenderer lineRenderer;
    public KO_PointChecker Checker;

    [SerializeField] private AudioSource BGaudsource;
    [SerializeField] private AudioClip correctDing;
    [SerializeField] private AudioClip wrongDing;

    List<int> wholeArrayInd;
    List<int> ArrayNum = new List<int>();
    List<WordCards> cardSlot = new List<WordCards>();
    private List<bool> LearnedArray = new List<bool>();
    int startIndex = 0;
    SetTypes gameDeck = SetTypes.Meat;
    //[SerializeField] private bool drawStuff = false;

    private float R = 60f;
    private float G = 180f;
    private float B = 20f;
    private float A = 255f;

    private Color correctColor;
    int Setamount = 6;
    private void Start()
    {
        correctColor = new Color(R / 255f, G / 255f, B / 255f,A/255f);
        loader = GameObject.FindGameObjectWithTag("loader").GetComponent<CardSetLoader>();
        myCard = GetComponent<Connect_Game>().AssignedCard;
        coll = GetComponent<BoxCollider2D>();
        startPos = this.transform.parent.position;
        lineRenderer = transform.parent.GetComponentInChildren<LineRenderer>();
        Checker = GameObject.FindGameObjectWithTag("checker").GetComponent<KO_PointChecker>();
        BGaudsource = GameObject.FindGameObjectWithTag("checker").GetComponent<AudioSource>();

        string wordData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/cardDatafile.txt").ToString();
        wholeArrayInd = wordData.Split(',').ToList().Select(int.Parse).ToList();

        for (int i = startIndex; i < startIndex + 2; i++)
        {
            ArrayNum.Add(wholeArrayInd[i]);
        }

        for (int i = startIndex; i < startIndex+2; i++)
        {
            cardSlot.Add(loader.Get_Set(gameDeck)[ArrayNum[i]]);
        }

        string boolData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
        string[] convertstep = boolData.Split(',').ToArray();
        if (convertstep.Length == 1)
        {
            LearnedArray = new List<bool> { false, false, false, false, false, false };
            string newboolData = String.Join(",", LearnedArray);
            File.WriteAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt", newboolData);
        }
        else
        {
            LearnedArray.Clear();
            for (int i = 0; i < Setamount; i++)
            {
                LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
            }
        }

    }

    public void CollidingDetect()
    {
        // Gets all hits
        RaycastHit2D[] allHits = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size + PlusSize,0f , Vector2.zero, Mathf.Infinity, layersToHit);
        // Assumes that it hit the wrong target
        bool hitCorrect = false;
        // Remember whatever target it hit, if it hit the correct one
        GameObject correctObject = null;

        // Goes through every target it hit
        foreach (RaycastHit2D hit in allHits)
        {
            // If it actually hit something, run this
            if (hit.collider != null)
            {
                // If the tag is the correct one, do this, otherwise mention to the dev that it didn't hit the correct thing
                if (hit.collider.CompareTag("DanishWord"))
                {
                    // Get whatever gameobject it hit
                    hitObject = hit.collider.gameObject;
                    // If it hit a gameobject with the correct assigned card, remember that it hit the correct thing
                    if (hitObject.GetComponent<Connect_Game>().AssignedCard == myCard)
                    {
                        hitCorrect = true;
                        correctObject = hitObject;
                        CheckShoplist(myCard);
                    }
                }
                else
                {
                    Debug.Log("Didn't hit text");
                }
                Debug.Log("I hit " + hit.collider.name);
            }
        }

        // If it had hit the correct thing, call the Correct() function, else do the inCorrect() function
        if (hitCorrect)
        {
            // Make sure the next code runs with the correct object, so that the line doesn't snap to a wrong one
            if (correctObject != null)
            {
                hitObject = correctObject;
            }
            else
            {
                Debug.LogError("Variable \"correctObject\" is unassigned");
            }
            Correct();
        }
        else
        {
            inCorrect();
        }
    }

    private void Correct()
    {

    Debug.Log("Correct");
        this.gameObject.tag = "Untagged";
        this.transform.position = hitObject.transform.position;
        lineRenderer.SetPosition(1, transform.position - transform.parent.position);
        lineRenderer.startColor = correctColor;
        lineRenderer.endColor = correctColor;
        BGaudsource.PlayOneShot(correctDing);
        Checker.AddPoints(1);
    }
    private void inCorrect()
    {

        Debug.Log("inCorrect");
        this.transform.position = startPos;
        BGaudsource.PlayOneShot(wrongDing);
        lineRenderer.SetPosition(1, transform.position - transform.parent.position);

    }

    private void CheckShoplist(WordCards card)
    {
        string boolData = File.ReadAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt").ToString();
        string[] convertstep = boolData.Split(',').ToArray();
        LearnedArray.Clear();
        for (int i = 0; i < Setamount; i++)
        {
            LearnedArray.Add(Convert.ToBoolean(convertstep[i]));
        }
        for (int i = startIndex; i < startIndex+2; i++)
        {
            if(cardSlot[i] == card)
            {
                LearnedArray[i] = true;
                string newboolData = String.Join(",", LearnedArray);
                File.WriteAllText(Application.dataPath + "/Resources/ShopListData/boolDatafile.txt", newboolData);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    if (drawStuff)
    //        Gizmos.DrawWireCube(coll.bounds.center, coll.bounds.size + PlusSize);

    //}


}
