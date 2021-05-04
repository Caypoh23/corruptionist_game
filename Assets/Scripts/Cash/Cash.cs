using System.Collections;
using EZCameraShake;
using Hand;
using JetBrains.Annotations;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cash
{
    public class Cash : MonoBehaviour
    {
        [SerializeField] private float amountCash;

        [SerializeField] private GameObject cashTextParent;
        [CanBeNull] [SerializeField] private GameObject pulsePanel;
        [CanBeNull] [SerializeField] private LevelController levelController;

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
                StartCoroutine(ActivatePulsePanel(2.0f));
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
            // можно вместо часов создать слайдер и на ui показывать что плюс 5 секунд добавилось
            // Нужно сделать так чтобы когда собирали деньги у мента, то в UI сумма показывала -100 а не +100, не знаю где ты это делала
            if (!(pulsePanel is null))
            {
                cashCount.OnCashRemove?.Invoke(amountCash);
                pulsePanel.SetActive(true);
                // need to think of a better method
                levelController._currentTimerValue -= 5.0f;
                // camera shake
                // may be we need to make it pulse not shake
                CameraShaker.Instance.ShakeOnce(.5f, 5f, .1f, 1f);
                yield return new WaitForSeconds(pulseDuration);
                pulsePanel.SetActive(false);
               
            }
        }
    }
}