using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject loading;

    public void OpenScene()
    {
        StartCoroutine(LoadScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
