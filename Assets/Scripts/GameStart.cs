using Hand;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel;

    [SerializeField] private HandGenerator handGenerator;
    [SerializeField] private GamePause gamePause;

    private ClockUI clock;

    private void Awake()
    {
        clock = FindObjectOfType<ClockUI>();
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
    }
}
