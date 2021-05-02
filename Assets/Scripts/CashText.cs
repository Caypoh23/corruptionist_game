using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class CashText : MonoBehaviour
{
    public TextMeshPro cashTextUI;

    // Set a prefab object to false in animator 
    private void SetActive()
    {
        gameObject.SetActive(false);
    }
}
