using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private Sprite[] switchSprites = new Sprite[2]; // 0 false, 1 true
    [SerializeField] private bool isOn;

    private Toggle _toggle;
    private Image _switchImage;
    private int _switchState;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }
    private void Start()
    {
        _switchState = isOn ? 1 : 0;
        _switchImage = _toggle.image;
        _switchImage.sprite = switchSprites[_switchState];
    }

    public void ToggleSprites()
    {
        isOn = !isOn;
        _switchState = 1 - _switchState;
        _switchImage.sprite = switchSprites[_switchState];
  
    }
}
