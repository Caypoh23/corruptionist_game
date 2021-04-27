using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class HandGenerator : MonoBehaviour
{
    [SerializeField] private HandStruct[] hands;
    [SerializeField] private float duration = 2;
    private int _minIndex = 0;
    private int _maxIndex;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var hand in hands)
            {
                hand.hand.transform.DOMove(hand.target.position, duration)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() =>
                    {
                        hand.hand.transform.DOMove(hand.initialPoisition.position, duration);
                    });
            }
        }
    }
}