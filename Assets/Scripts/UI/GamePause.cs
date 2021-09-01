using Money;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    // Наверное все нужно объеденить с sceneswitcher
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        [SerializeField] private GameObject[] panelsToHide;
        [SerializeField] private Sprite[] switchSprites;
        [SerializeField] private Button button;
        [SerializeField] private CashCount cashCount;
        [SerializeField] private bool isEndless;



        private Image switchImage;
        private SettingsMenu _settingsMenu;
        private bool _isPaused;
        private int _switchState; // 0 pause-icon , 1 play-icon
    
        private AudioManager _audioManager;

        // зачем панель туториала в паузе?
        //public void CloseTutorialPanel()
        //{
        //    tutorialPanel.SetActive(false);
        //    startGamePanel.SetActive(true);
        //    _isPaused = false;
        //}
        private void Awake()
        {
            _settingsMenu = FindObjectOfType<SettingsMenu>();
            _audioManager = FindObjectOfType<AudioManager>();
        }
        private void Start()
        {
            _switchState = 0;
            switchImage = button.image;
            switchImage.sprite = switchSprites[_switchState];

          
        }
        private void Update()
        {
            Time.timeScale = _isPaused ? 0 : 1;

        }
        public void TogglePause()
        {

            _switchState = 1 - _switchState;
            switchImage.sprite = switchSprites[_switchState];
            if (_isPaused)
            {
                pausePanel.SetActive(false);
               
                foreach (var panel in panelsToHide)
                {
                    panel.SetActive(false);
                }
                _audioManager.Play("officeBg");
                
                _audioManager.Play("clockTicking");

            }
            else
            {
                pausePanel.SetActive(true);
                _audioManager.Stop("officeBg");
            
                _audioManager.Stop("clockTicking");
                if (!isEndless)
                {
                    cashCount.SaveCashState();
                }
            }

            _isPaused = !_isPaused;
        }
    

        // for tutorial
        public void UnpauseGame()
        {
            _isPaused = false;
        }
        public void PauseGame()
        {
            _isPaused = true;
        }
    }
}