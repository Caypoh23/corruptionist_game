using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCollector : MonoBehaviour
{
    [SerializeField] private int cashAmount;
    // if the cash has been collected
    public bool _isCollected;
    
    public void OnMouseDown()
    {
        if (!_isCollected)
        {
            gameObject.SetActive(false);
            _isCollected = true;
            // cashAmount++;
        }
    }
    
    
}
