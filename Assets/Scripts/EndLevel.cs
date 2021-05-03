using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text dayNumberText;
    [SerializeField] private TMP_Text cashEarnedText;
    private Animator panelAnimator;

    private void Awake()
    {
        panelAnimator = panel.GetComponent<Animator>();
    }
    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        
    }
    public void ShowPanel(int dayNumber, float cashEarned)
    {
        dayNumberText.SetText("День " + dayNumber.ToString() + " завершен");
        cashEarnedText.SetText("Заработанно: " + cashEarned.ToString());
        panel.SetActive(true);
        panelAnimator.SetBool("hide", false);
    }
    //call from inspector (button event)
    public void NextLevel()
    {
        panelAnimator.SetBool("hide", true);
    }

}
