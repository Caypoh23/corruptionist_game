using System.Collections;
using EZCameraShake;
using Hand;
using JetBrains.Annotations;
using Level;
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

        [SerializeField] private bool isFlagged; // ment hand

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
            }
            else
            {
                _policeCaughtCounter.IncrementPoliceCaughtNumber();
                _handGenerator.BlockHandGenerator();
                _cashCount.OnCashRemove?.Invoke(amountCash); // - 200
            }
        }

        public void CashCanBeTaken()
        {
            _canBeTaken = true;
        }
    }
}