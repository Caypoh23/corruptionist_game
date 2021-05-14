using Hand;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private bool showTutorialPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject startGamePanel;

    private HandGenerator handGenerator;
    private GamePause gamePause;
    private ClockUI clock;
    private bool _isPaused;

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
            tutorialPanel.SetActive(true);
            
        }
        else
        {
           
            tutorialPanel.SetActive(false);
            handGenerator.UnblockHandGeneratorAfterWait();
            clock.StartClock();
        }
    }
    public void StartGame()
    {
        gamePause.UnpauseGame();
        tutorialPanel.SetActive(false);
        startGamePanel.SetActive(true);
        handGenerator.UnblockHandGeneratorAfterWait();
        clock.StartClock();
    }
}
