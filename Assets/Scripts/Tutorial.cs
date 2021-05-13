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
    private ClockUI clock;

    private void Awake()
    {
        handGenerator = FindObjectOfType<HandGenerator>();
        clock = FindObjectOfType<ClockUI>();
    }
    private void Start()
    {
        if (showTutorialPanel)
        {
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
        tutorialPanel.SetActive(false);
        startGamePanel.SetActive(true);
        handGenerator.UnblockHandGeneratorAfterWait();
        clock.StartClock();
    }
}
