using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public void ChangeLanguage(int langValue)
    {
        LocalizationManager.CurrentLanguage = "English";
    }
}
