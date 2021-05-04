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
        // cash text
        [SerializeField] private float amountCash;        
        [SerializeField] private GameObject cashTextParent; 

        [CanBeNull] [SerializeField] private GameObject pulsePanel;// TODO: MB action?
        [CanBeNull] [SerializeField] private LevelController levelController;

        [SerializeField] private bool isFlagged; // ment hand

        private SpriteRenderer _cashSR;

        private CashCount _cashCount;

        private HandGenerator _handGenerator;

        private TextMeshPro _cashText;

        private bool _canBeTaken;
        private string _plusOrMinus;
        private Color _textColor;

        private void Awake()
        {
            _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
            _cashSR = gameObject.GetComponent<SpriteRenderer>();
            _cashCount = FindObjectOfType<CashCount>();
            _handGenerator = FindObjectOfType<HandGenerator>();

            _canBeTaken = true;

            _plusOrMinus = isFlagged ? "-" : "+";
            _textColor = isFlagged ? new Color32(255, 0, 0, 255) : new Color32(48, 255, 0, 255);

            _cashText.SetText(_plusOrMinus + amountCash.ToString()); // +/- 200
            _cashText.color = _textColor;
        }

        public void OnMouseDown()
        {
            _cashSR.enabled = false;

            if (_canBeTaken)
            {
                // displaying text +/- 200
                cashTextParent.transform.position = transform.position;
                cashTextParent.SetActive(true);
                _canBeTaken = false; // no text will appear on second click
            }
            if (!isFlagged)
            {
                _cashCount.OnCashAdd?.Invoke(amountCash); // + 200
            }
            else
            {
                _handGenerator.BlockHandGenerator();
                //artCoroutine(ActivatePulsePanel(2.0f));
                _cashCount.OnCashRemove?.Invoke(amountCash); // - 200
            }


            //TODO: сделать так чтоб цифра денег спавнилась 1 раз а не 100500 раз пока игрок кликает на тригер
            // можно сделать через бул или установить количество клика на 1 раз используя int
            // это сделано?
        }

        public void CashCanBeTaken()
        {
            _canBeTaken = true;
        }

        private IEnumerator ActivatePulsePanel(float pulseDuration)
        {
            // можно вместо часов создать слайдер и на ui показывать что плюс 5 секунд добавилось
            // Нужно сделать так чтобы когда собирали деньги у мента, то в UI сумма показывала -100 а не +100, не знаю где ты это делала (на 36 строчке или где то там)
            if (!(pulsePanel is null))
            {
                //_cashCount.OnCashRemove?.Invoke(amountCash); this code is above 
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