using Money;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherWithSound : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Animator transition;
    [SerializeField] private Animator musicTransition;


    private void Start()
    {

        Time.timeScale = 1;
    }
    public void OpenScene()
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

        SceneManager.LoadSceneAsync(_sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}