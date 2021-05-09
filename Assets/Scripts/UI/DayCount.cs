using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCount : MonoBehaviour
{
    [SerializeField] private TMP_Text dayCountText;

    public void SetDayUI(int day) 
    {
        dayCountText.SetText("День: " + day.ToString());
    }
}
