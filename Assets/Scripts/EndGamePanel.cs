﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject moralePanel;


    // Start is called before the first frame update
    public void ShowMoralePanel()
    {
        moralePanel.SetActive(true);
       
    }

 
}
