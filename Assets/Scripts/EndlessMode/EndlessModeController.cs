using Data;
using Money;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class EndlessModeController : MonoBehaviour
{
    [SerializeField] private float workingDayTime;
    [SerializeField] private Clock clock;

    [SerializeField] private EndlessCashCount endlessCashCount; //TODO: mb event
    [SerializeField] private TMP_Text dayNumberText;
    [SerializeField] private PoliceCaughtCounter policeCaughtCounter;
    [SerializeField] private GameObject circleGameStartPanel;
    private bool _canBeClicked;

    [SerializeField] private EndlessHandGenerator _handGenerator;
    [SerializeField] private GamePause _gamePause;

    [SerializeField] private AudioManager _audioManager;


    public float _currentTimerValue;

    //private bool _canSpeedUpHands;
    private bool _timeCanTick;

    public int currentDay = 1;


    private void Awake()
    {
        dayNumberText.SetText(currentDay.ToString());
        _currentTimerValue = workingDayTime;
    }

    private void Start()
    {
        // progress bar fixing 
        //_canSpeedUpHands = true;
        _timeCanTick = true;
 
        _audioManager.Play("officeBg");
        _audioManager.Play("clockTicking");
    }

    private void Update()
    {
        LevelTimer();

        //if (currentDay == 8)
        //{
        //    _canSpeedUpHands = false;
        //}
    }

    private void LevelTimer()
    {
        if (_currentTimerValue > 0 && _timeCanTick)
        {
            _currentTimerValue -= Time.deltaTime;
            // progress bar start
            //progressBar.AnimateBar(_currentTimerValue);
        }
        else if (_currentTimerValue <= 0)
        {
            //currentLevel++;
            //loopNumber--;

            if (_timeCanTick)
            {
                StartCoroutine(Pulse());
            }
           
            _handGenerator.SpeedUpHands();
            _audioManager.SpeedUpForEndless("musicBg");
            //if (_canSpeedUpHands)
            //{
            //speed up hands
            //}

            _currentTimerValue = workingDayTime;
        }
    }

    public float GetWorkingDayTime()
    {
        return workingDayTime;
    }

    public void StopTimer()
    {
        _timeCanTick = false;
    }

    public void StartTimer()
    {
        _timeCanTick = true;
    }

    private IEnumerator Pulse()
    {
        for (float i = 1f; i < 1.2f; i += 0.05f)
        {
            dayNumberText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }

        dayNumberText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        currentDay++;
      
        dayNumberText.SetText(currentDay.ToString());

        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            dayNumberText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }

        dayNumberText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

}