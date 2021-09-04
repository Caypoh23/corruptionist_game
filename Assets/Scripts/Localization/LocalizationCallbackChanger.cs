using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Money;
using UnityEngine;

public class LocalizationCallbackChanger : MonoBehaviour
{
    [SerializeField] private CashCount cashCount;
    [SerializeField] private PoliceCaughtCounter policeCaughtCounter;

    public void OnModifyLocalization()
    {
        // if no MainTranslation then skip (most likely this localize component only changes the font)
        if (string.IsNullOrEmpty(Localize.MainTranslation))
            return;

        Localize.MainTranslation = Localize.MainTranslation.Replace("{EARNED_NUMBER}",
            cashCount.GetEarnedCash().ToString());
        Localize.MainTranslation = Localize.MainTranslation.Replace("{CAUGHT_COUNT}",
            policeCaughtCounter.GetOverallCaughtNumber().ToString());
    }
}
