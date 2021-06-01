using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    [SerializeField] private HandStruct[] hands;
    [SerializeField] private float handMovementTime;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void MoveHandForward()
    {
        _audioManager.Play("handSwoosh");
        // go to target
        hands[0].handGO.transform.DOMove(hands[0].target.position, handMovementTime)
            .SetEase(Ease.OutCubic);
    }

    public void MovePoliceHand()
    {
        _audioManager.Play("handSwoosh");
        hands[1].handGO.transform.DOMove(hands[1].target.position, handMovementTime)
            .SetEase(Ease.OutCubic);
    }


    public void MoveHandBack()
    {
        // go to initial position
        hands[0].handGO.transform
            .DOMove(hands[0].initialPosition.position, handMovementTime)
            .OnComplete(
                () =>
                {
                    // takes the last element from array
                    hands[0].cashGO.SetActive(true);
                    hands[0].cashGO.GetComponent<Money.Cash>().CashCanBeTaken();
                });
    }
}