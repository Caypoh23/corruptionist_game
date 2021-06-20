using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCashManager : MonoBehaviour
{
    [SerializeField] private float maxValue;
    [SerializeField] private EndlessCashProgressBar progressBar;

    private void Awake()
    {
        progressBar.SetMaxValue(maxValue);
        progressBar.SetValue(maxValue);
    }  
}
