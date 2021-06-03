using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hand;
using TMPro;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    [SerializeField] private HandStruct[] hands;
    [SerializeField] private float handMovementTime;
    public int currentTutorialNumber;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private BoxCollider2D panelCollider2D;
    private AudioManager _audioManager;

    private void Awake()
    {
        instructionsText.SetText("С первым рабочим днём, коллега!");
        fadePanel.SetActive(false);
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        currentTutorialNumber++;
        switch (currentTutorialNumber)
        {
            case 1:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Давай я введу тебя в курс дела.");
                break;
            case 2:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Принимай взятку как должное. Размер взятки не имеет значения.");
                
                panelCollider2D.enabled = false;
                fadePanel.SetActive(true);
                MoveHandForward(0);
                break;
            case 3:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Среди взяткодателей есть оборотни. Будь осторожен.");
                panelCollider2D.enabled = false;
                MoveHandForward(1);
                break;
            case 4:
                dialogPanel.SetActive(false);
                dialogPanel.SetActive(true);
                instructionsText.SetText("Хочешь оставаться в нём долго? Поднимай трубку, когда босс звонит.");
                panelCollider2D.enabled = false;
                MoveHandForward(2);
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
    }
}