using Hand;
using Money;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class TutorialPhonePickUp : MonoBehaviour
{

    private AudioManager _audioManager;
    [SerializeField] private CircleCollider2D collider2D;
    [SerializeField] private InteractiveTutorial interactiveTutorial;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    
        // do we need to block hand generator when we talk to secretary?
    }

    private bool _isPickedUp;

    private void OnMouseDown()
    {
        _isPickedUp = true;
       
        collider2D.enabled = false;

        _audioManager.Play("phonePickUp");
        _audioManager.Play("secretaryTalk");

        interactiveTutorial.StopSecretaryCall();
        //phonetalk
        StartCoroutine(WaitAndReturn());
        // pickUpSound.Play();

        //   talkSound.Play();
    }

    public bool CheckIfPhoneIsPickedUp()
    {
        return _isPickedUp;
    }

    public void ResetPhone()
    {
        _isPickedUp = false;
       
        collider2D.enabled = false;
        _audioManager.Stop("secretaryTalk");
    }

    private IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(6f);

        interactiveTutorial.MoveHandBack(2);
    }
    //TODO If not taken -200$
}