using System;
using UnityEngine;

namespace UI
{
    // Наверное все нужно объеденить с sceneswitcher
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private bool _isPaused;

        private void Update()
        {
            Time.timeScale = _isPaused ? 0 : 1;
        }

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            _isPaused = true;
        }

        public void ContinueGame()
        {
            pausePanel.SetActive(false);
            _isPaused = false;
        }
    }
}