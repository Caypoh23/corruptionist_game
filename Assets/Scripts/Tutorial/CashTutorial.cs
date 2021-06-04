using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashTutorial : MonoBehaviour
{
    [SerializeField] private GameObject clickParticle;
    [SerializeField] private GameObject cashTextParent;
    [SerializeField] private InteractiveTutorial interactiveTutorial;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private BoxCollider2D panelCollider2D;
    private TextMeshPro _cashText;
    [SerializeField] private bool isFlagged; 
    
    private bool _moneyAreTaken;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
    }

    private void OnMouseDown()
    {
        if (!isFlagged)
        {
          
            audioManager.Play("cash");
            _cashText.SetText("+100");
            _cashText.color = Color.green;
            interactiveTutorial.MoveHandBack(0);
          
           
        }
        else
        {
            audioManager.Play("police");
            _cashText.SetText("-200");
            _cashText.color = Color.red;
            interactiveTutorial.MoveHandBack(1);
        }
        panelCollider2D.enabled = true;
        // deactivate cash
        gameObject.SetActive(false);
        
        // particle
        Instantiate(clickParticle, transform.position, Quaternion.identity);

        // activate money
        cashTextParent.transform.position = transform.position;
        cashTextParent.SetActive(true);

        // move hand back
       // _interactiveTutorial.MoveHandBack();
       _moneyAreTaken = true;

    }

    public void CashCanBeTaken()
    {
        gameObject.SetActive(true);
        _moneyAreTaken = false;
    }
}