using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int numberOfworkingDays = 7;
    [SerializeField] private ClockUI clock; 
    [SerializeField] private EndLevel endLevel; //TODO: mb event
    [SerializeField] private CashCount cashCount; //TODO: mb event
    private float maxTimerValue;
    private float _currentTimerValue;

    
    [HideInInspector] public int loopNumber;
    private int currentLevel = 1;

    private void Awake()
    {
        maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
        loopNumber = numberOfworkingDays;
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
            //currentLevel++;
            //loopNumber--;
            //_currentTimerValue = maxTimerValue;
            clock.StopClock();
            endLevel.ShowPanel(currentLevel, cashCount.GetEarnedCash());
            //
            Debug.Log("Game over or start next level. Current level: " + currentLevel);
        }
    }

    public void StartNextlevel()
    {
        currentLevel++;
        loopNumber--;
        _currentTimerValue = maxTimerValue;
    }
}