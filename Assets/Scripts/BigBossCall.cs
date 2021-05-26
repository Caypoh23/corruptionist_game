using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossCall : MonoBehaviour
{
    [SerializeField] private int showNumberOfTimes;
    [SerializeField] private float holdTime;
    [SerializeField] private float handMovementTime = .5f;

    private float _elapsedHoldTime = 0.0f;

    private void Start()
    {
        //move forward, hold (mb shake hand animation), go back
        // if call is not asnwered = -200 money, 
        //if answered = nothing or +200
        // this is not dependant on hand generator
    }

}
