using System;
using UnityEngine;
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

        private Image switchImage;
        private bool _isPaused;
        private int _switchState; // 0 pause-icon , 1 play-icon

        // зачем панель туториала в паузе?
        //public void CloseTutorialPanel()
        //{
        //    tutorialPanel.SetActive(false);
        //    startGamePanel.SetActive(true);
        //    _isPaused = false;
        //}
        private void Update()
        {
            Time.timeScale = _isPaused ? 0 : 1;
            
        }
        private void Start()
        {
            _switchState = 0;
            switchImage = button.image;
            switchImage.sprite = switchSprites[_switchState];
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
            }
            else
            {
                pausePanel.SetActive(true);
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