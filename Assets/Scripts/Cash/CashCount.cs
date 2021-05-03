using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cash
{
    public class CashCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text cashCountText;

        private float cashCount;

        // Events for adding and removing money
        public Action<float> OnCashAdd;
        public Action<float> OnCashRemove;
        
        //TODO: mb event
        private void Start()
        {
            cashCountText.SetText("0");
        }

        private void OnEnable()
        {
            OnCashAdd += AddCash;
            OnCashRemove += RemoveCash;
            
        }

        private void OnDisable()
        {
            OnCashAdd -= AddCash;
            OnCashRemove -= RemoveCash;
        }
        
        private void AddCash(float amount)
        {
            StartCoroutine(Pulse(amount));
        }
        
        private void RemoveCash(float amount)
        {
            cashCount -= amount;
            cashCountText.SetText(cashCount.ToString());
        }
        
        private IEnumerator Pulse(float amount)
        {
            for (float i = 1f; i < 1.2f; i += 0.05f)
            {
                cashCountText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            cashCountText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            cashCount += amount;
            cashCountText.SetText(cashCount.ToString());

            for (float i = 1.2f; i >= 1f; i -= 0.05f)
            {
                cashCountText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            cashCountText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }

        public float GetEarnedCash()
        {
            return cashCount;
        }
    }
}