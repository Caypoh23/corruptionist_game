using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashTutorial : MonoBehaviour
{
    [SerializeField] private GameObject clickParticle;
    [SerializeField] private GameObject cashTextParent;
    private TextMeshPro _cashText;
    private AudioManager _audioManager;
    private InteractiveTutorial _interactiveTutorial;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _interactiveTutorial = FindObjectOfType<InteractiveTutorial>();
        _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
    }

    private void OnMouseDown()
    {
        _audioManager.Play("cash");

        // deactivate cash
        gameObject.SetActive(false);
        
        // particle
        Instantiate(clickParticle, transform.position, Quaternion.identity);

        // activate money
        cashTextParent.transform.position = transform.position;
        cashTextParent.SetActive(true);

        // move hand back
        _interactiveTutorial.MoveHandBack();
    }
}