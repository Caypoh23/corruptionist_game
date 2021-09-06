using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationSpriteChanger : MonoBehaviour
{
    [SerializeField] private Image continueImage;
    [SerializeField] private Button continueButton;
    [SerializeField] private Sprite enSprite;
    [SerializeField] private Sprite enSpritePressed;
    [SerializeField] private Sprite ruSprite;
    [SerializeField] private Sprite ruSpritePressed;
    public SpriteState spriteState = new SpriteState();

    private void Awake()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (LocalizationManager.CurrentLanguage == "English")
        {
            continueImage.sprite = enSprite;
            spriteState.pressedSprite = enSpritePressed;
        }
        else if (LocalizationManager.CurrentLanguage == "Russian")
        {
            continueImage.sprite = ruSprite;
            spriteState.pressedSprite = ruSpritePressed;
        }

        continueButton.spriteState = spriteState;
    }
}