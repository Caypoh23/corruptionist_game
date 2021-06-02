using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NextTutorial : MonoBehaviour
{
    [SerializeField] private int currentTutorialNumber;
    private InteractiveTutorial _interactiveTutorial;

    private void Awake()
    {
        _interactiveTutorial = FindObjectOfType<InteractiveTutorial>();
    }

    private void OnMouseDown()
    {
        currentTutorialNumber++;
        
        //_interactiveTutorial.MoveHandForward();
        // do your stuff
        // like if(currentTutorialNumber == 3) { interactiveTutorial.MoveHandForward(); }
        // do your stuff
        // like if(currentTutorialNumber == 4) { interactiveTutorial.MovePoliceHand(); }
        // or after the player took the ordinary hand we increment the currentTutorialNumber
        // maybe we should also disable collider of this object to be able to click hand collider
    }
}
