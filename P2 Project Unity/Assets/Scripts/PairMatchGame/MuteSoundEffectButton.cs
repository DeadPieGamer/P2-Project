using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundEffectButton : MonoBehaviour
{
    public Sprite UnMutedFXSprite;
    public Sprite MutedFXSprite;

    private Button _button;
    private SpriteState _state;

    void Start()
    {
        _button = GetComponent<Button>();
        if (GameSettings.Instance.IsSoundMutedPermanently())
        {
            _state.pressedSprite = MutedFXSprite;
            _state.highlightedSprite = MutedFXSprite;
            _button.GetComponent<Image>().sprite = MutedFXSprite;
        }
        else
        {
            _state.pressedSprite = UnMutedFXSprite;
            _state.highlightedSprite = UnMutedFXSprite;
            _button.GetComponent<Image>().sprite = UnMutedFXSprite;
        }
    }
    private void OnGUI()
    {
        if (GameSettings.Instance.IsSoundMutedPermanently())
            _button.GetComponent<Image>().sprite = MutedFXSprite;
        else
            _button.GetComponent<Image>().sprite = UnMutedFXSprite;
    }
    public void ToggleFXIcon()
    {
        if (GameSettings.Instance.IsSoundMutedPermanently())
        {
            _state.pressedSprite = UnMutedFXSprite;
            _state.highlightedSprite = UnMutedFXSprite;
            GameSettings.Instance.MuteSoundEffectPermanently(false);
        }
        else
        {
            _state.pressedSprite = MutedFXSprite;
            _state.highlightedSprite = MutedFXSprite;
            GameSettings.Instance.MuteSoundEffectPermanently(true);
        }
        _button.spriteState = _state;
    }

}
