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
    private bool _isPaused;

    private void Awake()
    {
        handGenerator = FindObjectOfType<HandGenerator>();
        clock = FindObjectOfType<ClockUI>();
       
        }
    private void Update()
    {
        Time.timeScale = _isPaused ? 0 : 1;
    }
    private void Start()
    {
        if (showTutorialPanel)
        {
            _isPaused = true;
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
        _isPaused = false;
        tutorialPanel.SetActive(false);
        startGamePanel.SetActive(true);
        handGenerator.UnblockHandGeneratorAfterWait();
        clock.StartClock();
    }
}
