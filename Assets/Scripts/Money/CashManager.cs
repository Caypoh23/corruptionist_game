using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Money
{
    public class CashManager : MonoBehaviour
    {
        [SerializeField] private CashProgressBar progressBar;

      
        private float totalCashCount;
        private Cash[] cashs;

        private void Awake()
        {
            cashs = FindObjectsOfType<Cash>();
            foreach (var cash in cashs)
            {
                totalCashCount += cash.GetCashAmount();
            }
            progressBar.SetMaxValue(totalCashCount * 7); // all cashCount * 7 
            progressBar.SetMaxValuePerLevel(totalCashCount);
            Debug.Log(totalCashCount);
        }
    }
}
