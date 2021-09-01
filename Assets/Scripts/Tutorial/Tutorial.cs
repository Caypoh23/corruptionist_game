using Hand;
using System.Collections;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private bool showTutorialPanel = false;
    [SerializeField] private GameObject[] tutorialPanels;
    [SerializeField] private GameObject startGamePanel;

    private HandGenerator handGenerator;
    private GamePause gamePause;
    private ClockUI clock;
    private bool _isPaused;
    private int currentPanelIndex = 0;
    private int _currentLaunchNumber;

    private void Awake()
    {
        showTutorialPanel = false;
        _currentLaunchNumber = DataManager.Instance.LoadLaunchNumber();
        currentPanelIndex = 0;
        handGenerator = FindObjectOfType<HandGenerator>();
        clock = FindObjectOfType<ClockUI>();
        gamePause = FindObjectOfType<GamePause>();
    }

    private void Start()
    {
        if (_currentLaunchNumber == 0 || showTutorialPanel)
        {
            gamePause.PauseGame();

            foreach (var panel in tutorialPanels)
            {
                panel.SetActive(false);
            }

            //tutorialPanels[currentPanelIndex].SetActive(true);
            StartCoroutine(WaitAndStartTurorial(currentPanelIndex));

            // increments current launch number only once and sets showTutorialPanel to false
            // further the boolean con be changed to true in settings menu 
            if (_currentLaunchNumber == 0)
            {
                _currentLaunchNumber++;
                showTutorialPanel = false;
                Debug.Log("current launch count number: " + _currentLaunchNumber);
                DataManager.Instance.SaveLaunchCount(_currentLaunchNumber);
            }
        }
        else
        {
            foreach (var panel in tutorialPanels)
            {
                panel.SetActive(false);
            }

            handGenerator.UnblockHandGeneratorAfterWait();
            clock.StartClock();
        }
    }

    private IEnumerator WaitAndStartTurorial(int panelIndex)
    {
        yield return new WaitForSeconds(1f);
        tutorialPanels[panelIndex].SetActive(true);
    }
    public void StartGame()
    {
        gamePause.UnpauseGame();

        foreach (var panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        startGamePanel.SetActive(true);
        handGenerator.UnblockHandGeneratorAfterWait();
        clock.StartClock();
    }

    public void NextPanel()
    {
        tutorialPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex++;
        tutorialPanels[currentPanelIndex].SetActive(true);
    }

    public void ToggleTutorial(bool isOn)
    {
        showTutorialPanel = isOn;
    }
}