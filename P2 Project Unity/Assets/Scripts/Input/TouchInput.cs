using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TouchInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount ; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position); //whatever i is at this point in our loop, that is the touch we want to get and for each we are getting position
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        }
    }
}
