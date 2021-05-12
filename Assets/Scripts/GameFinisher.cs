using System.Collections;
using System.Collections.Generic;
using Money;
using Level;
using TMPro;
using UI;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    // думаю что можно было бы сделать это в level Controller или end level точно хз
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Animator player;
    [SerializeField] private GameObject moralePanel;
    [SerializeField] private Animator moraleAnimator;
    [SerializeField] private CashCount cashCount;
    [SerializeField] private PoliceCaughtCounter policeCaughtCounter;
    [SerializeField] private TMP_Text overallCaughtTimesText;
    [SerializeField] private TMP_Text cashEarnedText;
    [SerializeField] private GameObject pulsePanel;

    #region Cache

    private static readonly int ShowText = Animator.StringToHash("ShowText");

    #endregion

    public void BurstPlayer()
    {
        Debug.Log("BOOOOM");
        pulsePanel.SetActive(true);
        //player.SetTrigger("blowUp");
    }

    public void ShowMoralePanel()
    {
        moralePanel.SetActive(true);
        moraleAnimator.SetTrigger(ShowText);
    }

    public void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
        // не деактивируется
        // при открытии финальной панели, моральная панель долждна отключаться
        moralePanel.SetActive(false);
        cashEarnedText.SetText("Заработанно в общем: " + cashCount.GetEarnedCash());
        overallCaughtTimesText.SetText("Пойман в общем: " + policeCaughtCounter.GetOverallCaughtNumber() + " раз");
    }
}