using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageClickSFX : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private Image _sprite;
    [SerializeField] private Color _color = new Color32 (65, 156, 0, 255);

 
    private Color _originalColor;


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _originalColor = _sprite.color;
    }

    public void OnClick()
    {
        audioManager.Play("buttonClick");
        _sprite.color = _color;
    }
    public void OnUnClick()
    {
        _sprite.color = _originalColor;
    }

}
