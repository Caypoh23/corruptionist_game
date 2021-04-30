using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float maxTimerValue;
    [SerializeField] private HandGenerator handGenerator;
    [SerializeField] private int loopNumber;
    
    private float _currentTimerValue;

    private void Awake()
    {
        _currentTimerValue = maxTimerValue;
    }

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
            Debug.Log("Game over or start next level. Current level: " + currentLevel + " " + handGenerator.CanPlay);
        }
        // Think of better method
        else if(loopNumber == 0)
        {
            handGenerator.CanPlay = false;
        }
    }
    
    
}