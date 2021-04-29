using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HandGenerator : MonoBehaviour
{
    [SerializeField] private HandStruct[] hands;
    [SerializeField] private float duration = 2;
    private float speed = 1;
    private int _index;

    private void Awake()
    {
    }

    private void Update()
    {
        InvokeRepeating($"MoveHandLerp", 0f, 0.2f);
        hands[_index].cashGO.SetActive(true);
    }

    /*private void MoveHand()
    {
        _index = Random.Range(0, hands.Length);
        hands[_index].handGO.transform.DOMove(hands[_index].target.position, duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                hands[_index].handGO.transform.DOMove(hands[_index].initialPosition.position, duration);
            });
    }*/

    private void MoveHandLerp()
    {
        _index = Random.Range(0, hands.Length);
        hands[_index].handGO.transform.position = Vector3.Lerp(hands[_index].initialPosition.position,
            hands[_index].target.position,
            Mathf.PingPong(Time.time / duration, 2));
    }
}