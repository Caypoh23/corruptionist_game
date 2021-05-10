using System.Collections;
using EZCameraShake;
using Hand;
using JetBrains.Annotations;
using Level;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Money
{
    public class Cash : MonoBehaviour
    {
        // cash text
        [SerializeField] private float amountCash;
        [SerializeField] private GameObject cashTextParent;

        [SerializeField] private bool isFlagged; // ment hand

        private CashProgressBar _progressBar; //TODO: action/event 100%

        private CashCount _cashCount;
        private PoliceCaughtCounter _policeCaughtCounter;

        private HandGenerator _handGenerator;

        private TextMeshPro _cashText;
        private bool _canBeTaken;
        private string _plusOrMinus;
        private Color _textColor;


        private void Awake()
        {
            _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
            _cashCount = FindObjectOfType<CashCount>();
            _policeCaughtCounter = FindObjectOfType<PoliceCaughtCounter>();
            _handGenerator = FindObjectOfType<HandGenerator>();
            _progressBar = FindObjectOfType<CashProgressBar>();
            _canBeTaken = true;

            _plusOrMinus = isFlagged ? "-" : "+";
            _textColor = isFlagged ? new Color32(255, 0, 0, 255) : new Color32(48, 255, 0, 255);

            _cashText.SetText(_plusOrMinus + amountCash.ToString()); // +/- 200
            _cashText.color = _textColor;
        }

        public void OnMouseDown()
        {
            gameObject.SetActive(false);

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
                _progressBar.AddValue(amountCash);
            }
            else
            {
                _policeCaughtCounter.IncrementPoliceCaughtNumber();
                _handGenerator.BlockHandGeneratorByMent();
                _cashCount.OnCashRemove?.Invoke(amountCash); // - 200
                _progressBar.RemoveValue(amountCash);
            }
        }

        public void CashCanBeTaken()
        {
            _canBeTaken = true;
        }
        //return cash amount
        public float GetCashAmount()
        {
            return isFlagged ? 0 : amountCash;
        }
    }
}