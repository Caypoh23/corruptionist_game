using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using I2.Loc;
using Money;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndlessGameOver : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject endExitPanel;
    [SerializeField] private TMP_Text reasonText;
    [SerializeField] private EndlessHandGenerator handGenerator;
    [SerializeField] private Clock clock;
    [SerializeField] private EndlessCashCount endlessCashCount;
    [SerializeField] private TMP_Text cashRecord;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private EndlessCashProgressBar progressBar;
    [SerializeField] private EndlessModeController endlessController;
    [SerializeField] private Localize localizeReason;

    // maybe to add ads play time (counter) and limit to 3 continues
    // (if died 3 times and watched 3 ads to continues, there is no more chance watch ad again)

    [SerializeField] private ContinueGameAds continueGameAds;


    private float _recordCashCount;

    private void Awake()
    {
        _recordCashCount = DataManager.Instance.LoadRecord();
        pauseCanvas.enabled = true;
        endExitPanel.SetActive(false);
    }

    private void SaveRecordCashState()
    {
        DataManager.Instance.SaveRecordState((int) _recordCashCount);
    }

    // show lose panel and reason 

    //--

    // caught by ment

    // progress bar went 0

    // thats it

    public void EndGame(string reason)
    {
        if (_recordCashCount < endlessCashCount.currentCashCount)
        {
            _recordCashCount = endlessCashCount.currentCashCount;
            SaveRecordCashState();
        }

        DataManager.Instance.LoadRecord();
        cashRecord.SetText(_recordCashCount.ToString());
        pauseCanvas.enabled = false;
        endGamePanel.SetActive(true);

        Time.timeScale = 0;

        progressBar.StopDecrementValue();
        clock.StopClock();
        endlessController.StopTimer();
        handGenerator.BlockHandGenerator();

        localizeReason.SetTerm(reason);
        //reasonText.SetText(reason);
    }

    public void ShowAd()
    {
        continueGameAds.PlayContinueGameAd(ContinueGame);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;

        endGamePanel.SetActive(false);

        // dunno which method to use to set max value // setValue 
        progressBar.SetValue(1000);
        clock.StartClock();
        endlessController.StartTimer();
        progressBar.StartDecrementValue();
        handGenerator.UnblockHandGenerator();
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