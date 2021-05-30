using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PauseGameForEndLevel : MonoBehaviour
{
    private GamePause _gamePause;

    private void Awake()
    {
        _gamePause = FindObjectOfType<GamePause>();
    }
    public void PauseGameForEndlevelFromAnimator()
    {
        _gamePause.PauseGame();
    }
}
