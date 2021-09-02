using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using TMPro;
using UI;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    /// <summary>
    /// WARNING: SPAGETTI CODE
    /// </summary>
    [SerializeField] private HandTutorialStruct[] hands;
    [SerializeField] private float handMovementTime;
    public int currentTutorialNumber;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject clickAnywhere;

    [SerializeField] private GameObject startGamePanel;

    [SerializeField] private BoxCollider2D panelCollider2D;
    private AudioManager _audioManager;
    private bool _canShowArrow;

    [Header("Arrow")]

    [SerializeField] private GameObject waves;

    [SerializeField] private GameObject noClickArrow;

    [SerializeField] private float speed;
    [SerializeField] private TutorialPhonePickUp phone;
    [SerializeField] private GameObject secretaryPenaltyText;

  
    private GamePause gamePause;
    private TextMeshPro _cashText;

    private void Awake()
    {
        

        instructionsText.SetText("Congratulations on the first working day!");
        StartCoroutine(WaitAndShowClick());
        fadePanel.SetActive(false);
        _audioManager = FindObjectOfType<AudioManager>();

        _cashText = secretaryPenaltyText.GetComponentInChildren<TextMeshPro>();
        _cashText.SetText("-300");
        _cashText.color = Color.red;

        


    }

    private IEnumerator WaitAndStartTurorial()
    {
        clickAnywhere.SetActive(false);
        yield return new WaitForSeconds(1f);

        dialogPanel.SetActive(true);
       
    }

    private void Start()
    {
        StartCoroutine(WaitAndStartTurorial());
        _audioManager.Play("officeBg");
        _audioManager.Play("clockTicking");
    }
    private void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);

        foreach (var hand in hands)
        {
            hand.pointerArrow.transform.position = Vector3.Lerp(hand.iconTarget1.position, hand.iconTarget2.position, time);
        }
       
    }

    private void OnMouseDown()
    {
        currentTutorialNumber++;
        clickAnywhere.SetActive(false);
        switch (currentTutorialNumber)
        {
            case 1:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Let me tell you how things are done.");
                StartCoroutine(WaitAndShowClick());
                break;
            case 2:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Take money from people, it doesn't matter how much bribe is.");
                
                panelCollider2D.enabled = false;
                fadePanel.SetActive(true);
                MoveHandForward(0);
                waves.transform.position = hands[0].iconTarget2.position;
                StartCoroutine(WaitAndShowArrow(0));
               
                break;
            case 3:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Police Officers will also give you bribes. Be carefull, don't get caught!");
                panelCollider2D.enabled = false;
                MoveHandForward(1);

                noClickArrow.transform.position = hands[1].iconTarget2.position;
                StartCoroutine(WaitAndShowArrow(1));
                
             

                StartCoroutine(WaitAndReturn(1, 3));
               
                break;
            case 4:
                _audioManager.Play("phoneRing");
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Sometimes Big Boss will call you. Make sure to answer his call.");
                panelCollider2D.enabled = false;
                MoveHandForward(2);
             
                waves.transform.position = hands[2].iconTarget2.position;
                StartCoroutine(WaitAndShowArrow(2));

                StartCoroutine(WaitAndReturn(2, 4));
                break;
            case 5:
                panelCollider2D.enabled = false;
                startGamePanel.SetActive(true);
                Debug.Log("END");
                break;
        }

        // do your stuff
        // like if(currentTutorialNumber == 3) { interactiveTutorial.MoveHandForward(); }
        // do your stuff
        // like if(currentTutorialNumber == 4) { interactiveTutorial.MovePoliceHand(); }
        // or after the player took the ordinary hand we increment the currentTutorialNumber
        // maybe we should also disable collider of this object to be able to click hand collider
    }

    private void MoveHandForward(int index)
    {
        _audioManager.Play("handSwoosh");
        // go to target
        hands[index].handGO.transform.DOMove(hands[index].target.position, handMovementTime)
            .SetEase(Ease.OutCubic);
       
    }

    public void MoveHandBack(int index)
    {
        hands[index].pointerArrow.SetActive(false);
        waves.SetActive(false);
        noClickArrow.GetComponent<Animator>().SetTrigger("hide");
        // go to initial position
        hands[index].handGO.transform
            .DOMove(hands[index].initialPosition.position, handMovementTime)
            .OnComplete(
                () =>
                {
                    // takes the last element from array
                    hands[index].cashGO.SetActive(true);
                    hands[index].cashGO.GetComponent<Money.Cash>().CashCanBeTaken();
                   
                });

        StartCoroutine(WaitAndShowClick());

        if(index == 2)
        {
            panelCollider2D.enabled = true;
        }
       
    }


    public IEnumerator WaitAndReturn(int index, int time)
    {
        yield return new WaitForSeconds(time);
       
        MoveHandBack(index);
        panelCollider2D.enabled = true;
        _audioManager.Stop("secretaryTalk");

        if (index == 2 && !phone.CheckIfPhoneIsPickedUp())
        {
            _audioManager.Stop("phoneRing");
            secretaryPenaltyText.transform.position = hands[2].cashGO.transform.position;
            secretaryPenaltyText.SetActive(true);
            phone.ResetPhone();
           
        }
      
    }   

    public void StopSecretaryCall()
    {
        hands[2].pointerArrow.SetActive(false);
        waves.SetActive(false);
        _audioManager.Stop("phoneRing");
      
    }
    private IEnumerator WaitAndShowArrow(int index)
    {
        yield return new WaitForSeconds(.5f);
        hands[index].pointerArrow.SetActive(true);

        if (index == 1)
        {
            noClickArrow.SetActive(true);
        }
        else
        {
            waves.SetActive(true);
        }



    }

    private IEnumerator WaitAndShowClick()
    {
        yield return new WaitForSeconds(.7f);
        clickAnywhere.SetActive(true);
    }
}