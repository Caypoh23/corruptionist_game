using Data;
using Hand;
using Money;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class EndlessModeController : MonoBehaviour
{
    [SerializeField] private float workingDayTime;
    [SerializeField] private Clock clock;

    [SerializeField] private CashCount cashCount; //TODO: mb event
    [SerializeField] private DayCount dayCount;
    [SerializeField] private PoliceCaughtCounter policeCaughtCounter;
    [SerializeField] private GameObject circleGameStartPanel;
    private bool _canBeClicked;

    [SerializeField] private HandGenerator _handGenerator;
    [SerializeField] private GameFinisher _gameFinisher;
    [SerializeField]  private GamePause _gamePause;
    [SerializeField]  private CashManager _cashManager;
    [SerializeField]  private CashProgressBar _cashProgressBar;
    [SerializeField]  private AudioManager _audioManager;


    public float _currentTimerValue;

    public int currentDay;

    private void Awake()
    {
        currentDay = DataManager.Instance.LoadLevelNumber();

        dayCount.SetDayUI(currentDay);

        //maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
        _currentTimerValue = workingDayTime;
      
    }

    private void Start()
    {
        // progress bar fixing 
    
        _audioManager.Play("officeBg");
        _audioManager.Play("clockTicking");
    }

    private void Update()
    {
        LevelTimer();
    }

    private void LevelTimer()
    {
        if (_currentTimerValue > 0)
        {
            _currentTimerValue -= Time.deltaTime;
            // progress bar start
            //progressBar.AnimateBar(_currentTimerValue);
        }
        else if (_currentTimerValue <= 0)
        {
            //currentLevel++;
            //loopNumber--;
            
            currentDay++;
            _currentTimerValue = workingDayTime;
        }

  
    }

    public float GetWorkingDayTime()
    {
        return workingDayTime;
    }


}
