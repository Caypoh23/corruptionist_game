using Cash;
using UI;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private int numberOfWorkingDays = 7;
        [SerializeField] private ClockUI clock; 
        [SerializeField] private EndLevel endLevel; //TODO: mb event
        [SerializeField] private CashCount cashCount; //TODO: mb event
        [SerializeField] private GameFinisher gameFinisher;
        private float maxTimerValue;
        public float _currentTimerValue;

    
        [HideInInspector] public int loopNumber;
        private int currentLevel = 1;

        private void Awake()
        {
            maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
            loopNumber = numberOfWorkingDays;
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
                endLevel.OnShowPanel?.Invoke(currentLevel, cashCount.GetEarnedCash());
                Debug.Log("Game over or start next level. Current level: " + currentLevel);
            }

            if(currentLevel == 7 && _currentTimerValue <= 0)
            {
                gameFinisher.BurstPlayer();
          
            }
        }

        public void StartNextLevel()
        {
            currentLevel++;
            loopNumber--;
            _currentTimerValue = maxTimerValue;
        }
    
        public int GetCurrentLevel()
        {
            return currentLevel; // 
        }
        public int GetTotalLevels()
        {
            return numberOfWorkingDays; // 7
        }
    }
}