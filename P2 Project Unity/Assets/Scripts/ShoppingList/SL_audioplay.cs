using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SL_audioplay : MonoBehaviour
{
    [SerializeField] private AudioSource SLaudioSource;
    [SerializeField] private AudioClip SLaudioClip;

    private WordCards SLcard;
    private SL_ListItem _listitem;
    void Start()
    {
        _listitem = GetComponentInParent<SL_ListItem>();
        SLaudioSource = GameObject.FindGameObjectWithTag("checker").GetComponent<AudioSource>();
    }

    public void PlayAssign_clip()
    {
        SLcard = _listitem.itemCard;
        SLaudioClip = SLcard.word_Audio;
        SLaudioSource.PlayOneShot(SLaudioClip);
    }
}
