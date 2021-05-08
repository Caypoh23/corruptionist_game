using Cash;
using Hand;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ClockUI clock;
        [SerializeField] private EndLevel endLevel; //TODO: mb event
        [SerializeField] private CashCount cashCount; //TODO: mb event
        [SerializeField] private GameFinisher gameFinisher;
        [SerializeField] private PoliceCaughtCounter policeCaughtCounter;
        private HandGenerator _handGenerator;
        private GameFinisher _gameFinisher;
        private float maxTimerValue;
        public float _currentTimerValue;

        private LevelItemGenerator itemGenerator;
        public int currentLevel = 1;

        private void Awake()
        {
            _gameFinisher = FindObjectOfType<GameFinisher>();
            _handGenerator = FindObjectOfType<HandGenerator>();
            maxTimerValue = clock.GetSecondsPerIngameWorkingDay();
            _currentTimerValue = maxTimerValue;
            itemGenerator = FindObjectOfType<LevelItemGenerator>();
        }

        private void Update()
        {
            LevelTimer();
        }

        // Rename method name
        private void LevelTimer()
        {
            if (_currentTimerValue > 0)
            {
                _currentTimerValue -= Time.deltaTime;
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
        
        // нужно подумать о том как бы отключить jail 
        // потому что если мы взяли последним элементом мусорскую руку
        // то опускается jail но если при этом кончился таймер
        // то jail до сих пор играет свою анимацию
        // и когда нажимаем на кнопку продолжить то видим там jail
        public void StartNextLevel()
        {
            currentLevel++;
            _currentTimerValue = maxTimerValue;
            policeCaughtCounter.todayCaughtTimes = 0;
            itemGenerator.LoadItems();
            
        }

        public int GetCurrentLevel()
        {
            return currentLevel; // 
        }
    }
}