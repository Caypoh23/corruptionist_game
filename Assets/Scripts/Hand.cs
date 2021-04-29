using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private int cashAmount;
    [SerializeField] private GameObject cashObject;

    public void OnMouseDown()
    {
        cashObject.SetActive(false);
    }
}