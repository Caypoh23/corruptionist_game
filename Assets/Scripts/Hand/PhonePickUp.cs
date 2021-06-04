using Hand;
using Money;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class PhonePickUp : MonoBehaviour
{
    private HandGenerator _handGenerator;
    private AudioManager _audioManager;
    [SerializeField] private CircleCollider2D collider2D;
    [SerializeField] private GameObject clickParticle;
    private bool _isPickedUp;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _handGenerator = FindObjectOfType<HandGenerator>();
        // do we need to block hand generator when we talk to secretary?
    }



    private void OnMouseDown()
    {
        Instantiate(clickParticle, transform.position, Quaternion.identity);
        _isPickedUp = true;
        _handGenerator.BlockHandGenerator();
        collider2D.enabled = false;

        _audioManager.Play("phonePickUp");
        _audioManager.Play("secretaryTalk");
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
        _handGenerator.UnblockHandGenerator();
        collider2D.enabled = true;
        _audioManager.Stop("secretaryTalk");
    }
    //TODO If not taken -200$
}