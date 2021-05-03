using System.Collections;
using EZCameraShake;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cash
{
    public class Cash : MonoBehaviour
    {
        [SerializeField] private float amountCash;

        [SerializeField] private GameObject cashTextParent;
        [CanBeNull] [SerializeField] private GameObject pulsePanel;

        [SerializeField] private bool isFlagged;

        private SpriteRenderer _cashSR;
        private bool _canBeTaken;

        private CashCount cashCount;

        private TextMeshPro _cashText;

        private void Awake()
        {
            _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
            _cashSR = gameObject.GetComponent<SpriteRenderer>();
            _canBeTaken = true;
            _cashText.SetText("+" + amountCash.ToString());
            cashCount = FindObjectOfType<CashCount>();
        }

        public void OnMouseDown()
        {
            _cashSR.enabled = false;
            if (_canBeTaken && !isFlagged)
            {
        
                cashTextParent.transform.position = transform.position;
                cashTextParent.SetActive(true);
                cashCount.OnCashAdd?.Invoke(amountCash);
                _canBeTaken = false;
            }
            if (isFlagged)
            {
                StartCoroutine(ActivatePulsePanel(4.5f));
            }

            //TODO: сделать так чтоб цифра денег спавнилась 1 раз а не 100500 раз пока игрок кликает на тригер
            // можно сделать через бул или установить количество клика на 1 раз используя int
        }

        public void CashCanBeTaken()
        {
            _canBeTaken = true;
        }

        private IEnumerator ActivatePulsePanel(float pulseDuration)
        {
            // need to think of a method to stop collection action
            // Нужно сделать так чтобы когда собирали деньги у мента, то в UI сумма показывала -100 а не +100, не знаю где ты это делала
            if (!(pulsePanel is null))
            {
                cashCount.OnCashRemove?.Invoke(amountCash);
                pulsePanel.SetActive(true);
                // camera shake
                CameraShaker.Instance.ShakeOnce(.5f, 5f, .1f, 1f);
                yield return new WaitForSeconds(pulseDuration);
                pulsePanel.SetActive(false);
               
            }
        }
    }
}