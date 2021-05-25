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
    [SerializeField] private GameObject pulsePanel;

    [SerializeField] private Animator playerAnim;


    [Header("Timers")] [SerializeField]
    private float timeBeforeHeartBeat; // sidebar shake - and red/white (indicating that is super full)

    [SerializeField] private float timeForHeartBeat;
    [SerializeField] private float timeForBurst;
    [SerializeField] private float timeBeforeEnd;
    private float _elapsedTime = 0f;
    private bool _hasShaken;

    #region Cache

    private static readonly int ShowText = Animator.StringToHash("ShowText");

    #endregion

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