using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using Level;
using Money;
using TMPro;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BigBossCall : MonoBehaviour
{
    [SerializeField] private int[] showLevels;
    [SerializeField] private float holdTime;
    [SerializeField] private float phoneTalkTime;
    [SerializeField] private float handMovementTime = .5f;
    [SerializeField] private HandStruct handStruct;
    [SerializeField] private Animator anim;
    [SerializeField] private PhonePickUp phone;


    [Header("Cash Text")] 
    
    [SerializeField] private float amountCashToMinus;
    [SerializeField] private GameObject cashTextParent;
    [SerializeField] private Transform cashTextTransform;
    private LevelController _levelController;
    private CashCount _cashCount;
    private TextMeshPro _cashText;
    private CashProgressBar _progressBar;
    private AudioManager _audioManager;

    private bool _canGoBack;
    private bool _canMoveHand;

    private bool _wasShown;
    private bool _penaltyWasShown;
    private bool _canPlayAnim;
    private bool _hasCallRang;
    private bool _wasAnswered;

    private float _elapsedHoldTime = 0.0f;
    private float _elapsedWaitBeforeShowTime = 0.0f;
    private float _elapsedPhoneTalkTime = 0.0f;


    private void Awake()
    {
        _cashCount = FindObjectOfType<CashCount>();
        _levelController = FindObjectOfType<LevelController>();
        _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
        _progressBar = FindObjectOfType<CashProgressBar>();
        _audioManager = FindObjectOfType<AudioManager>();

        _cashText.SetText("- " + amountCashToMinus.ToString()); // - 200
        _cashText.color = new Color32(255, 0, 0, 255);
        _cashText.fontSize = 200;
        _penaltyWasShown = false;
    }

    private void Start()
    {
        //move forward, hold (mb shake hand animation), go back
        // if call is not asnwered = -200 money, 
        //if answered = nothing or +200
        // this is not dependant on hand generator
        // TODO: maxLevel
    }

    private void Update()
    {
        foreach (var level in showLevels)
        {
            if (level == _levelController.currentLevel)
            {
                //_wasShown = false;
                // _penaltyWasShown = false;

                _elapsedWaitBeforeShowTime += Time.deltaTime;
                if (_elapsedWaitBeforeShowTime >= _levelController.maxTimerValue / 2 - 1)
                {
                    if (!_hasCallRang)
                    {
                        _audioManager.Play("secretaryCall");
                        _hasCallRang = true;
                    }
                }

                if (_elapsedWaitBeforeShowTime >= _levelController.maxTimerValue / 2)
                {
                    //_elapsedHoldTime = 0.0f;
                    MoveForward();
                    _canMoveHand = true;

                    //if phone is answered
                    if (phone.CheckIfPhoneIsPickedUp())
                    {
                        _wasAnswered = true;
                        
                        if (_hasCallRang)
                        {
                            _audioManager.Stop("secretaryCall");
                        }

                        _canPlayAnim = false;
                        _elapsedPhoneTalkTime += Time.deltaTime;
                        _canGoBack = false;
                        if (_elapsedPhoneTalkTime >= phoneTalkTime)
                        {
                            _canGoBack = true;
                            phone.ResetPhone(); // isPickedUp = false
                        }
                    }

                    // if call was not answered - show text - 300
                    if (_wasShown && !_penaltyWasShown && !_wasAnswered)
                    {
                        _elapsedWaitBeforeShowTime = 0.0f;
                        _elapsedPhoneTalkTime = 0.0f;
                        _elapsedHoldTime = 0.0f;

                        cashTextParent.transform.position = cashTextTransform.position;
                        cashTextParent.SetActive(true);
                        _cashCount.OnCashRemove?.Invoke(amountCashToMinus); // - 200
                        _progressBar.RemoveValue(amountCashToMinus);

                        _penaltyWasShown = true;
                    }

                    MoveBack();
                    //sound
                    anim.SetBool("wiggle", _canPlayAnim);
                }
            }
        }
    }

    private void MoveForward()
    {
        _elapsedWaitBeforeShowTime += Time.deltaTime;
        if (_canMoveHand && !_wasShown)
        {
            handStruct.handGO.transform.DOMove(handStruct.target.position, handMovementTime)
                .SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    _canPlayAnim = true;
                    _canGoBack = true;
                });
        }
    }


    private void MoveBack()
    {
        _elapsedHoldTime += Time.deltaTime;
        //var waitTime = wasPickedUp ? holdTime + phoneTalkTime : holdTime;


        if (_canGoBack && _elapsedHoldTime >= holdTime)
        {
            _canPlayAnim = false;
            // go to initial position
            handStruct.handGO.transform.DOMove(handStruct.initialPosition.position, handMovementTime).OnComplete(
                () =>
                {
                    _canMoveHand = false;
                    _canGoBack = false;
                    _wasShown = true;
                });
        }
    }

    public void RemoveHand()
    {
        _audioManager.Stop("phoneRing");
        _audioManager.Stop("secretaryCall");
        MoveBack();
    }
    public void ResetTimers()
    {
        _elapsedWaitBeforeShowTime = 0.0f;
        _elapsedHoldTime = 0.0f;
        _elapsedPhoneTalkTime = 0.0f;
        _wasShown = false;
        _hasCallRang = false;
        _wasAnswered = false;
        _penaltyWasShown = false;
        _canPlayAnim = false;
        // если и это не сработало то еще по обнулять надо будет
    }
}