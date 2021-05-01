using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class Cash : MonoBehaviour
{
    [SerializeField] private TextMeshPro textUI;
    [SerializeField] private Hand hand;

    private void Start()
    {
        hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        //TODO: нужно чтобы определенный cashAmount сеттился имеено на определенный textUI
        // сейчас он сеттит только 1 значение даже если у всех разная сумма.
        textUI.SetText("+" + hand.cashAmount.ToString());
    }

    // Set a prefab object to false in animator 
    private void SetActive()
    {
        gameObject.SetActive(false);
    }
}
