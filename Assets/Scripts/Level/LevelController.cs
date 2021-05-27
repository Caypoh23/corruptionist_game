using System;
using Money;
using Hand;
using System.Collections;
using Data;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        public float maxTimerValue;
        [SerializeField] private ClockUI clock;
        [SerializeField] private EndLevel endLevel; //TODO: mb event
        [SerializeField] private CashCount cashCount; //TODO: mb event
        [SerializeField] private DayCount dayCount;
        [SerializeField] private GameFinisher gameFinisher;
        [SerializeField] private PoliceCaughtCounter policeCaughtCounter;
        private bool _canBeClicked;

        [SerializeField] private HandGenerator _handGenerator;
        private GameFinisher _gameFinisher;
        private CashManager _cashManager;
        private CashProgressBar _cashProgressBar;


        public float _currentTimerValue;

        private LevelItemGenerator itemGenerator;
        public int currentLevel;

        private void Awake()
        {
            currentLevel = DataManager.Instance.LoadLevelNumber();
            dayCount.SetDayUI(currentLevel);
            _gameFinisher = FindObjectOfType<GameFinisher>();
            _handGenerator = FindObjectOfType<HandGenerator>();
            //maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
            _currentTimerValue = maxTimerValue;
            itemGenerator = FindObjectOfType<LevelItemGenerator>();

            _cashManager = FindObjectOfType<CashManager>();
            _cashProgressBar = FindObjectOfType<CashProgressBar>();
        }

        private void Start()
        {
            // progress bar fixing 
            //_cashProgressBar.AddValue();
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
            else if (_currentTimerValue <= 0 && currentLevel != 7)
            {
                //currentLevel++;
                //loopNumber--;
                //_currentTimerValue = maxTimerValue;
                clock.StopClock();
                endLevel.OnShowPanel?.Invoke(currentLevel, cashCount.GetEarnedDailyCash(),
                    policeCaughtCounter.GetTodayCaughtNumber());
                _canBeClicked = true;
                Debug.Log("Game over or start next level. Current level: " + currentLevel);
            }

            if (currentLevel >= 7 && _currentTimerValue <= 0)
            {
                // stop hands movement
                _handGenerator.StopHands();
                _handGenerator.DeactivateJail();
                clock.StopClock();
                gameFinisher.FinishGame(); // все методы тут
                DataManager.Instance.DeleteCashState();
                DataManager.Instance.DeleteLevelNumber();
                

                //gameFinisher.ShowMoralePanel(); открываетсә с аниматора 
            }
        }

        public void StartNextLevel()
        {
            if (_canBeClicked)
            {
                currentLevel++;
                DataManager.Instance.SaveLevelNumber(currentLevel);
                dayCount.SetDayUI(currentLevel);
                clock.StartClock();
                _currentTimerValue = maxTimerValue;
                policeCaughtCounter.todayCaughtTimes = 0;
                itemGenerator.LoadItems();
                _handGenerator.DeactivateJail();
                _handGenerator.OnLevelUp();
                _cashProgressBar.FillMaxValuePerLevel(_cashManager.totalCashCount * currentLevel);
                _canBeClicked = false;
            }
        }
    }
}