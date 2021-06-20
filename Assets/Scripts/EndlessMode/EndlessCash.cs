using Money;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndlessCash : MonoBehaviour
{
    // cash text
    [SerializeField] private float amountCash;
    [SerializeField] private GameObject cashTextParent;

    [SerializeField] private bool isFlagged; // ment hand
    [SerializeField] private bool isCandy; // child hand
    [SerializeField] private GameObject clickParticle;


    [SerializeField] private EndlessCashProgressBar _progressBar; //TODO: action/event 100%

    private CashCount _cashCount;
    private PoliceCaughtCounter _policeCaughtCounter;

    private EndlessHandGenerator _handGenerator;
    private AudioManager _audioManager;
    private TextMeshPro _cashText;
    private bool _canBeTaken;
    private string _plusOrMinus;
    private Color _textColor;


    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _cashCount = FindObjectOfType<CashCount>();
        _policeCaughtCounter = FindObjectOfType<PoliceCaughtCounter>();
        _handGenerator = FindObjectOfType<EndlessHandGenerator>();
        _progressBar = FindObjectOfType<EndlessCashProgressBar>();

        _cashText = cashTextParent.GetComponentInChildren<TextMeshPro>();
        _canBeTaken = true;

        _plusOrMinus = isFlagged ? "-" : "+";
        _textColor = isFlagged ? new Color32(255, 0, 0, 255) : new Color32(48, 255, 0, 255);

        _cashText.SetText(_plusOrMinus + amountCash.ToString()); // +/- 200
        _cashText.color = _textColor;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            if (isCandy)
            {
                _audioManager.Play("candy");
            }
            else if (isFlagged)
            {
                _audioManager.Play("police");
                Debug.Log("Lose");
            }
            else
            {
                _audioManager.Play("cash");

            }

            gameObject.SetActive(false);
            // дерьмово выглядит мой партикл
            Instantiate(clickParticle, transform.position, Quaternion.identity);

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