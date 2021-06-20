using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class EndlessStartGame : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel;

    [SerializeField] private EndlessHandGenerator handGenerator;
    [SerializeField] private GamePause gamePause;
    [SerializeField] private EndlessCashProgressBar endlessCashProgressBar;
    [SerializeField] private Clock clock;

    private void Awake()
    {
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gamePause.UnpauseGame();
       
        startGamePanel.SetActive(true);
    
        handGenerator.UnblockHandGeneratorAfterWait();
        clock.StartClock();
        endlessCashProgressBar.StartDecrementValue();
    }
}