using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOrTutorial : MonoBehaviour
{
    private int _currentLaunchNumber;

    private void Awake()
    {
        _currentLaunchNumber = DataManager.Instance.LoadLaunchNumber();
    }

    public void StartGame()
    {
        if (_currentLaunchNumber == 0)
        {
            _currentLaunchNumber++;

            DataManager.Instance.SaveLaunchCount(_currentLaunchNumber);
            SceneManager.LoadSceneAsync("TutorialScene");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
}
