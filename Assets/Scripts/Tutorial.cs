using Hand;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private bool showTutorialPanel;
    [SerializeField] private GameObject[] tutorialPanels;
    [SerializeField] private GameObject startGamePanel;

    private HandGenerator handGenerator;
    private GamePause gamePause;
    private ClockUI clock;
    private bool _isPaused;
    private int currentPanelIndex = 0;

    private void Awake()
    {
        handGenerator = FindObjectOfType<HandGenerator>();
        clock = FindObjectOfType<ClockUI>();
        gamePause = FindObjectOfType<GamePause>();
        }
  
    private void Start()
    {
        if (showTutorialPanel)
        {
            gamePause.PauseGame();
            foreach (var panel in tutorialPanels)
            {
                panel.SetActive(false);
            }
            tutorialPanels[currentPanelIndex].SetActive(true);
            
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
}
