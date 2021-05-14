using System;
using UnityEngine;

namespace UI
{
    // Наверное все нужно объеденить с sceneswitcher
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private bool _isPaused;

        // зачем панель туториала в паузе?
        //public void CloseTutorialPanel()
        //{
        //    tutorialPanel.SetActive(false);
        //    startGamePanel.SetActive(true);
        //    _isPaused = false;
        //}

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void ContinueGame()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}