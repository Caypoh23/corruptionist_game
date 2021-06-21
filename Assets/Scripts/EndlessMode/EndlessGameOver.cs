using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameOver : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject endExitPanel;
    [SerializeField] private TMP_Text reasonText;
    [SerializeField] private EndlessHandGenerator handGenerator;
    [SerializeField] private Clock clock;

    private void Awake()
    {
        endExitPanel.SetActive(false);
    }
    // show lose panel and reason 

    //--

    // caught by ment

    // progress bar went 0

    // thats it

    public void EndGame(string reason)
    {
        endGamePanel.SetActive(true);
        Time.timeScale = 0;
        clock.StopClock();
        handGenerator.BlockHandGenerator();
        reasonText.SetText(reason);
    }

    public void GoHome()
    {
        StartCoroutine(WaitAndSwitchScene("MainMenu"));

    }
    public void Restart()
    {
        StartCoroutine(WaitAndSwitchScene("EndlessScene"));

    }
    private IEnumerator WaitAndSwitchScene(string sceneName)
    {
        endExitPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void ShakeCamera()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 5f, 0.1f, 1.0f);
    }


}
