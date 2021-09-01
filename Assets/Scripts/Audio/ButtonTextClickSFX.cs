using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextClickSFX : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _color = new Color32(65, 156, 0, 255);

    private float _originalTextSize;
    private Color _originalColor;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        //_originalTextSize = _text.fontSize;
        _originalColor = _text.color;
    }

    public void OnClick()
    {
        audioManager.Play("buttonClick");
        _text.color = _color;
        //_text.fontSize = _originalTextSize + 5;
    }
    public void OnUnClick()
    {
        _text.color = _originalColor;
        //_text.fontSize = _originalTextSize;
    }


}
