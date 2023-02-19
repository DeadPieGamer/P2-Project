//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestTouch : MonoBehaviour
//{
//    //Credit to Samyam Youtube
//    private InputManager inputManager;
//    private Camera cameraMain;

//    private void Awake()
//    {
//        inputManager = InputManager.Instance();
//        cameraMain = Camera.main;
//    }

//    private void OnEnable()
//    {
//        inputManager.OnStartTouch += Move;
//    }

//    private void OnDisable()
//    {
//        inputManager.OnEndTouch -= Move;
//    }

//    public void Move(Vector2 screenposition, float time)
//    {
//        Vector3 screenCoordinates = new Vector3(screenposition.x, screenposition.y, cameraMain.nearClipPlane);
//        Vector3 worldcoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
//        worldcoordinates.z = 0;
//        transform.position = worldcoordinates;
//    }

//}
