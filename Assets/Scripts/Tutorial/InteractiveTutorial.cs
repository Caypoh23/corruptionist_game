using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using TMPro;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    [SerializeField] private InteractiveTutorialStruct[] interactiveTutorials;
    [SerializeField] private GameObject curruptionist;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Animator dialogAnim;
    [SerializeField] private TMP_Text dialogTextField;
    [SerializeField] private GameObject fadePanel;

    [SerializeField] private float readTime;

    private Collider2D _collider;
    private bool _panelIsShown;

    private int _tutorialIndex = 0;
    private int _instructionIndex = 0;
    private int _handIndex = 0;

    private float _panelShowTime = 0.5f;

    private float _elapsedTime = 0.0f;
    private float _elapsedReadTime = 0.0f;
    private float _elapsedShowHandTime = 0.0f;

    private AudioManager _audioManager;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        _collider.enabled = false;

        _tutorialIndex = 0;
        _instructionIndex = 0;

        dialog.SetActive(false);
        curruptionist.SetActive(false);
        fadePanel.SetActive(false);

        dialogTextField.SetText(interactiveTutorials[_tutorialIndex].instructions[_instructionIndex]);// first string of text
    }
    private void Update()
    {
        curruptionist.SetActive(true);


        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= _panelShowTime)
        {
            dialogTextField.SetText(interactiveTutorials[_tutorialIndex].instructions[_instructionIndex]);// first string of text
            dialog.SetActive(true);
           
        }

        _elapsedReadTime += Time.deltaTime;
        if(_elapsedReadTime >= readTime)
        {
            if (interactiveTutorials[_tutorialIndex].hands.Length == 0)
            {
                _collider.enabled = true; // next tutprial step can be clicked
            }
            else
            {
                fadePanel.SetActive(true);

                MoveHandForward(
                    interactiveTutorials[_tutorialIndex].hands[_handIndex],
                    interactiveTutorials[_tutorialIndex].handMovementTime
                    );
            }
           
        }

    }
    private void OnMouseDown()
    {
        if (_instructionIndex >= interactiveTutorials[_tutorialIndex].instructions.Length - 1)
        {
            _tutorialIndex++;
            _instructionIndex = 0;
        }
        else
        {
            _instructionIndex++;
        }


        _elapsedTime = 0.0f;

        dialogAnim.SetTrigger("hideDialog");
    

    }
 
    //[SerializeField] private HandStruct[] hands;
    //[SerializeField] private float handMovementTime;
    //private AudioManager _audioManager;

    //private void Awake()
    //{
    //    _audioManager = FindObjectOfType<AudioManager>();
    //}

    public void MoveHandForward(HandStruct hand, float handMovementTime)
    {
        _audioManager.Play("handSwoosh");
        // go to target
        hand.handGO.transform.DOMove(hand.target.position, handMovementTime)
            .SetEase(Ease.OutCubic);
    }

    //public void MovePoliceHand()
    //{
    //    _audioManager.Play("handSwoosh");
    //    hands[1].handGO.transform.DOMove(hands[1].target.position, handMovementTime)
    //        .SetEase(Ease.OutCubic);
    //}


    public void MoveHandBack(HandStruct hand, float handMovementTime)
    {
        // go to initial position
        hand.handGO.transform
            .DOMove(hand.initialPosition.position, handMovementTime)
            .OnComplete(
                () =>
                {
                    // takes the last element from array
                    hand.cashGO.SetActive(true);
                    hand.cashGO.GetComponent<Money.Cash>().CashCanBeTaken();
                });
    }
}