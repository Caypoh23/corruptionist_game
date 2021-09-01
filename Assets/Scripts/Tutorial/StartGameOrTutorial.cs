using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOrTutorial : MonoBehaviour
{
    private int _currentLaunchNumber;
    [SerializeField] private Animator transition;
    [SerializeField] private Animator musicTransition;

    private void Awake()
    {
        _currentLaunchNumber = DataManager.Instance.LoadLaunchNumber();
    }
    public void StartGame()
    {
        StartCoroutine(TransitionFade());
    }
    IEnumerator TransitionFade()
    {
        transition.SetTrigger("fadeout");
        musicTransition.SetTrigger("fadeout");

        yield return new WaitForSeconds(1f);

        ChangeScene();
    }

    private void ChangeScene()
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
