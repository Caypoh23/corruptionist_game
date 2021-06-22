using System;
using System.Collections;
using Data;
using TMPro;
using UnityEngine;

namespace Money
{
    public class EndlessCashCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text cashCountText;

        [HideInInspector] public float currentCashCount;

        // Events for adding and removing money
        public Action<float> OnCashAdd;
        //public Action<float> OnCashRemove;

        // TODO: mb event
        private void Start()
        {
            cashCountText.SetText(currentCashCount.ToString());
        }

        private void OnEnable()
        {
            OnCashAdd += AddCash;
            //OnCashRemove += RemoveCash;
        }

        private void OnDisable()
        {
            OnCashAdd -= AddCash;
            //OnCashRemove -= RemoveCash;
        }

        private void AddCash(float amount)
        {
            StartCoroutine(Pulse(amount));
        }

        /*private void RemoveCash(float amount)
        {
            _currentCashCount -= amount;
            if (_currentCashCount < 0)
            {
                _currentCashCount = 0;
            }
            
            cashCountText.SetText(_currentCashCount.ToString());
        }*/

        private IEnumerator Pulse(float amount)
        {
            for (float i = 1f; i < 1.2f; i += 0.05f)
            {
                cashCountText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            cashCountText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            currentCashCount += amount;
            cashCountText.SetText(currentCashCount.ToString());

            for (float i = 1.2f; i >= 1f; i -= 0.05f)
            {
                cashCountText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            cashCountText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}