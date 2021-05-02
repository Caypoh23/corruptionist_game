using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cash : MonoBehaviour 
{ 
    [SerializeField] private float amountCash;

    private SpriteRenderer _cashSR;
    private bool _canBeTaken;

    [SerializeField] private GameObject cashTextParent;
    private CashCount cashCount;

    private TextMeshPro _cashText;

    private void Awake()
    {
        _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
        _cashSR = gameObject.GetComponent<SpriteRenderer>();
        _canBeTaken = true;
        _cashText.SetText("+" + amountCash.ToString());
        cashCount = FindObjectOfType<CashCount>();
    }


    public void OnMouseDown()
    {
        _cashSR.enabled = false;
        if (_canBeTaken)
        {  cashTextParent.transform.position = transform.position;
            cashTextParent.SetActive(true);
            cashCount.addCash(amountCash);
            _canBeTaken = false;
        }
        
        //TODO: сделать так чтоб цифра денег спавнилась 1 раз а не 100500 раз пока игрок кликает на тригер
        // можно сделать через бул или установить количество клика на 1 раз используя int
    }

    public void CashCanBeTaken()
    {
        _canBeTaken = true;
    }
}