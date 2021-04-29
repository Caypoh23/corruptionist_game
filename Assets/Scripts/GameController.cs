using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float maxTimerValue;
    private float _currentTimerValue;

    private void Awake()
    {
        _currentTimerValue = maxTimerValue;
    }

    private void Update()
    {
        if (_currentTimerValue > 0)
        {
            _currentTimerValue -= Time.deltaTime;
            Debug.Log("Game over or start next level");
        }
    }
    
    
}