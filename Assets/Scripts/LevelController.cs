using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float maxTimerValue;

    private float _currentTimerValue;

    public int loopNumber;

    private void Update()
    {
        LevelTimer();
    }

    // Rename method name
    private void LevelTimer()
    {
        if (_currentTimerValue > 0 && loopNumber != 0)
        {
            _currentTimerValue -= Time.deltaTime;
        }
        else if (_currentTimerValue <= 0)
        {
            currentLevel++;
            loopNumber--;
            _currentTimerValue = maxTimerValue;
            Debug.Log("Game over or start next level. Current level: " + currentLevel);
        }
    }
}