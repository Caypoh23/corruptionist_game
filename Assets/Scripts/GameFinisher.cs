using System;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using Money;
using Level;
using TMPro;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameFinisher : MonoBehaviour
{
    // думаю что можно было бы сделать это в level Controller или end level точно хз -хуйня идея
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject endPanel; // nado razdelit eto dermo

    [SerializeField] private GameObject pulsePanel;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator moraleAnimator;

    [SerializeField] private CashCount cashCount;
    [SerializeField] private PoliceCaughtCounter policeCaughtCounter; //статистику в отдельный скрипт
    [SerializeField] private TMP_Text overallCaughtTimesText;
    [SerializeField] private TMP_Text cashEarnedText;
    [SerializeField] private Camera camera;


    [Header("Timers")] 
    [SerializeField]
    private float timeBeforeHeartBeat; // sidebar shake - and red/white (indicating that is super full)

    [SerializeField] private float timeForHeartBeat;
    [SerializeField] private float timeForBurst;
    [SerializeField] private float timeBeforeEnd;
    private float _elapsedTime = 0f;
    private bool _hasShaken;

    #region Cache

    private static readonly int ShowText = Animator.StringToHash("ShowText");

    #endregion

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void FinishGame()
    {
        // сначала анимация ударов сердца должа сыграться
        // потом камера должна дергаться и в этот же момент 
        // коррупционер играет анимацию смерти.
        // после того как он умер останавливается музыка
        // открывается панель морали

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= timeBeforeHeartBeat)
        {
            pulsePanel.SetActive(true);
            if (!_hasShaken)
            {
                ShakeCamera();
            }
        

            if (_elapsedTime >= timeForHeartBeat + timeBeforeHeartBeat)
            {
                pulsePanel.SetActive(false);
                StopCamera();
                //camera
              

                if (_elapsedTime >= timeForBurst + timeForHeartBeat + timeBeforeHeartBeat)
                {
                    //particles
                    BurstPlayer();


                    if (_elapsedTime >= timeBeforeEnd + timeForBurst + timeForHeartBeat + timeBeforeHeartBeat)
                    {
                        ShowClosingEndPanel();
                        _elapsedTime = 0f;
                    }
                }
            }
        }
    }
   
    private void ShakeCamera()
    {
        CameraShaker.Instance.DefaultPosInfluence = new Vector3(.02f, .02f, .02f);
        CameraShaker.Instance.DefaultRotInfluence = new Vector3(.02f, .02f, .02f);
        CameraShaker.Instance.ShakeOnce(5f, 10f, .1f, .2f);
      
    }
    private void StopCamera()
    {
        _hasShaken = true;
    }
    private void BurstPlayer()
    {
        Debug.Log("BOOOOM");

        playerAnim.SetTrigger("burst");
    }

    private void ShowClosingEndPanel()
    {
        endPanel.SetActive(true);
        //moraleAnimator.SetTrigger(ShowText);
        //typing effect
        
        
    }
   
    private void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);

       
        cashEarnedText.SetText("Заработанно в общем: " + cashCount.GetEarnedCash());
        overallCaughtTimesText.SetText("Пойман в общем: " + policeCaughtCounter.GetOverallCaughtNumber() + " раз");
    }

    
}