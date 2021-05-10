using Cash;
using Hand;
using System.Collections;
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
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private HandGenerator _handGenerator;
        private GameFinisher _gameFinisher;

        public float _currentTimerValue;

        private LevelItemGenerator itemGenerator;
        public int currentLevel = 1;

        private void Awake()
        {
            _gameFinisher = FindObjectOfType<GameFinisher>();
            _handGenerator = FindObjectOfType<HandGenerator>();
            //maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
            _currentTimerValue = maxTimerValue;
            itemGenerator = FindObjectOfType<LevelItemGenerator>();
        }

        private void Update()
        {
            LevelTimer();
        }


        // overall piece of a shit method, cause calling it in f* update
        // Rename method name
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
                endLevel.OnShowPanel?.Invoke(currentLevel, cashCount.GetEarnedCash(),
                    policeCaughtCounter.GetTodayCaughtNumber());
                Debug.Log("Game over or start next level. Current level: " + currentLevel);
            }

            if (currentLevel >= 7 && _currentTimerValue <= 0)
            {
                // stop hands movement
                _handGenerator.StopHands();
                gameFinisher.BurstPlayer();
                gameFinisher.ShowMoralePanel();
            }
        }

        public void StartNextLevel()
        {
            currentLevel++;
            dayCount.SetDayUI(currentLevel);
            _currentTimerValue = maxTimerValue;
            policeCaughtCounter.todayCaughtTimes = 0;
            itemGenerator.LoadItems();
            _handGenerator.DeactivateJail();
            _handGenerator.OnLevelUp();
        }

        public int GetCurrentLevel()
        {
            return currentLevel; // 
        }
    }
}