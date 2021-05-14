using Money;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoralePanel : MonoBehaviour
{
  
    [SerializeField] private TMP_Text moraleTextField;
    [TextArea]
    [SerializeField] private string moraleText;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject statsPanel;

    [SerializeField] private TMP_Text overallCaughtTimesText;
    [SerializeField] private TMP_Text cashEarnedText;
    private CashCount cashCount;
    private PoliceCaughtCounter policeCaughtCounter;

    private void Awake()
    {
        cashCount = FindObjectOfType<CashCount>();
        policeCaughtCounter = FindObjectOfType<PoliceCaughtCounter>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        moraleTextField.SetText(moraleText);
    }
    public void ShowEndGamePanel()
    {
        cashEarnedText.SetText("Заработанно в общем: " + cashCount.GetEarnedCash());
        overallCaughtTimesText.SetText("Пойман в общем: " + policeCaughtCounter.GetOverallCaughtNumber() + " раз");
        statsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
