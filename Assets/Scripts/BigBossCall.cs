using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using Level;
using Money;
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
    private LevelController _levelController;
    private CashCount _cashCount;


    private bool _canGoBack;
    private bool _canMoveHand;

    private bool _wasShown;
    private bool _canPlayAnim;

    private float _elapsedHoldTime = 0.0f;
    private float _elapsedWaitBeforeShowTime = 0.0f;
    private float _elapsedPhoneTalkTime = 0.0f;

    private void Awake()
    {
       
        _cashCount = FindObjectOfType<CashCount>();
        _levelController = FindObjectOfType<LevelController>();
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
            if(level == _levelController.currentLevel)
            {
                _elapsedWaitBeforeShowTime += Time.deltaTime;
                if(_elapsedWaitBeforeShowTime >= _levelController.maxTimerValue / 2)
                {
                    MoveForward();
                    _canMoveHand = true;
                    if (phone.CheckIfPhoneIsPiCkedUp())
                    {
                        _canPlayAnim = false;
                        _elapsedPhoneTalkTime += Time.deltaTime;
                        _canGoBack = false;
                        if(_elapsedPhoneTalkTime >= phoneTalkTime)
                        {
                            _canGoBack = true;
                            phone.ResetPhone(); // isPickedUp = false
                        }
                    }
                  
                    MoveBack();
                    anim.SetBool("wiggle", _canPlayAnim);

                }
               

            }
        }

     
    }
    private IEnumerator WaitBeforeShowHand(float time)
    {
        yield return new WaitForSeconds(time);
        _canMoveHand = true;
    }
    private void MoveForward()
    {

        _elapsedWaitBeforeShowTime += Time.deltaTime;
        if (_canMoveHand && !_wasShown )
        {
            handStruct.handGO.transform.DOMove(handStruct.target.position, handMovementTime)
                   .SetEase(Ease.OutCubic).OnComplete(() => { _canPlayAnim = true; _canGoBack = true; }) ;
        }
    }


    private void MoveBack()
    {
        _elapsedHoldTime += Time.deltaTime;

        if (_canGoBack && _elapsedHoldTime >= holdTime)
        {
            _canPlayAnim = false;
            // go to initial position
            handStruct.handGO.transform.DOMove(handStruct.initialPosition.position, handMovementTime).OnComplete(
                () => { _canMoveHand = false; _canGoBack = false; _wasShown = true; });
           

        }
    }

    
}