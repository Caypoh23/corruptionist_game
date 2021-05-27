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
    [SerializeField] private int showNumberOfTimes;
    [SerializeField] private float holdTime;
    [SerializeField] private float handMovementTime = .5f;
    [SerializeField] private HandStruct handStruct;
    private LevelController _levelController;
    private bool _canGoBack;
    private bool _canMoveHand;
    private CashCount _cashCount;

    private float _elapsedHoldTime = 0.0f;

    private void Awake()
    {
        _canMoveHand = true;
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
        if (_canMoveHand)
        {
            if (_levelController.currentLevel == 3 || _levelController.currentLevel == 5)
            {
                handStruct.handGO.transform.DOMove(handStruct.target.position, handMovementTime)
                    .SetEase(Ease.OutCubic).OnComplete(() => _canGoBack = true);
                _elapsedHoldTime += Time.deltaTime;
               
                if (_canGoBack && _elapsedHoldTime >= holdTime)
                {
                    // go to initial position
                        handStruct.handGO.transform.DOMove(handStruct.initialPosition.position, handMovementTime).OnComplete(
                            () => { _canMoveHand = false;  _canGoBack = false;});
                      
                    
                }
            }
        }
    }
}