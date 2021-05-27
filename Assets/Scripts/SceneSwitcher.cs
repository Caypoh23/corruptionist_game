using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Start()
    {
        
        Time.timeScale = 1;
    }

    public void OpenScene()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}