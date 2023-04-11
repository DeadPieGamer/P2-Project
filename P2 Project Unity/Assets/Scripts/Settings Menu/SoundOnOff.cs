using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundOnOff : MonoBehaviour
{
    private Sprite soundOnImage;
    [SerializeField] public Sprite soundOffImage;
    public Button button;
    private bool isOn = true;


    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        soundOnImage = button.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOffImage;
            isOn = false;

        }
        else
        {
            button.image.sprite = soundOnImage;
            isOn = true;

        }

        SetLevel(isOn);
    }


    public void SetLevel(bool onOrOff)
    {
        // 0.0001 = off
        // 1 = on
        if (onOrOff)
        {
            mixer.SetFloat("SoundVol", Mathf.Log10(1) * 20);
        }
        else
        {
            mixer.SetFloat("SoundVol", Mathf.Log10(0.0001f) * 20);
        }
    }
}
