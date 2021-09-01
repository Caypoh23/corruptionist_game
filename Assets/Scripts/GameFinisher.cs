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
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject flashGamePanel;
    [SerializeField] private GameObject pulsePanel;

    [SerializeField] private Animator playerAnim;


    [Header("Timers")] [SerializeField]
    private float timeBeforeHeartBeat; // sidebar shake - and red/white (indicating that is super full)

    [SerializeField] private float timeForHeartBeat;
    [SerializeField] private float timeForBurst;
    [SerializeField] private float timeBeforeEnd;
    // звук для камера шейк
    private AudioManager _audioManager;
    private CashProgressBar _cashProgressBar;
    private float _elapsedTime = 0f;
    private bool _hasShaken;
    private bool _hasFlashed;
    private bool _hasDoomBellPlayed;
    #region Cache

    private static readonly int ShowText = Animator.StringToHash("ShowText");

    #endregion
    private void Awake()
    {
        flashGamePanel.SetActive(false);
        _audioManager = FindObjectOfType<AudioManager>();
        _cashProgressBar = FindObjectOfType<CashProgressBar>();
    }

    public void FinishGame()
    {
        // сначала анимация ударов сердца должа сыграться
        // потом камера должна дергаться и в этот же момент 
        // коррупционер играет анимацию смерти.
        // после того как он умер останавливается музыка
        // открывается панель морали

        // doom sound + fill progress bar
        // text "you are fucked"
        // shaking 
        // text "thats it"
        if (!_hasDoomBellPlayed)
        {
            _audioManager.Play("doomBell");
            _hasDoomBellPlayed = true;
        }
        _cashProgressBar.AddValue(1000);
        _audioManager.Stop("officeBg");
        _audioManager.Stop("musicBg");
        _audioManager.Stop("clockTicking");

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= timeBeforeHeartBeat)
        {
            if (!_hasFlashed)
            {
                flashGamePanel.SetActive(true);
                
            }

            if (_elapsedTime >= timeForHeartBeat + timeBeforeHeartBeat)
            {
                _hasFlashed = true;
                flashGamePanel.SetActive(false);

                if (!_hasShaken)
                { 

                    ShakeCamera();
                    
                    BurstPlayer();
                }
             
                //camera


                if (_elapsedTime >= timeForBurst + timeForHeartBeat + timeBeforeHeartBeat)
                {
                    //particles

                    StopCamera();


                    if (_elapsedTime >= timeBeforeEnd + timeForBurst + timeForHeartBeat + timeBeforeHeartBeat)
                    {
                        ShowClosingEndPanel();
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
       
        endGamePanel.SetActive(true);

        //moraleAnimator.SetTrigger(ShowText);
        //typing effect
    }
}