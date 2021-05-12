using Money;
using Hand;
using Level;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private GameObject levelPanel;
        [SerializeField] private TMP_Text dayNumberText;
        [SerializeField] private TMP_Text cashEarnedText;
        [SerializeField] private TMP_Text cashEarnedTotalText;
        [SerializeField] private TMP_Text cashLostTotalText;
        [SerializeField] private TMP_Text caughtTimesText;

        private Animator panelAnimator;

        public Action<int, float, int> OnShowPanel;

        private HandGenerator _handGenerator;

        private CashCount _cashCount;
        //private PoliceCaughtCounter _policeCaughtCounter;

        private void OnEnable()
        {
            OnShowPanel += ShowPanel;
        }

        private void OnDisable()
        {
            OnShowPanel -= ShowPanel;
        }

        private static readonly int Hide = Animator.StringToHash("hide");

        private void Awake()
        {
            panelAnimator = levelPanel.GetComponent<Animator>();
            _handGenerator = FindObjectOfType<HandGenerator>();
            _cashCount = FindObjectOfType<CashCount>();
            // _policeCaughtCounter = FindObjectOfType<PoliceCaughtCounter>();
        }

        private void Start()
        {
            levelPanel.SetActive(false);
        }

        public void ShowPanel(int dayNumber, float cashEarned, int caughtTimes)
        {
            dayNumberText.SetText("День " + dayNumber.ToString() + " завершен");
            cashEarnedText.SetText(cashEarned.ToString());
            cashEarnedTotalText.SetText(_cashCount.GetEarnedCash().ToString());

            cashLostTotalText.SetText(_cashCount.GetLostCash().ToString());
            caughtTimesText.SetText(caughtTimes.ToString());
            levelPanel.SetActive(true);
            panelAnimator.SetBool(Hide, false);
            _handGenerator.BlockHandGenerator();
        }

        public void DeactivatePanel()
        {
            levelPanel.SetActive(false);
        }

        //call from inspector (button event)
        public void NextLevel()
        {
            panelAnimator.SetBool(Hide, true); //setactive false from animator
            // Нужно 100% отключать панель след уровня. иначе кнопка паузы не работает во первых
            // во вторых это неправльно оставлять включенным панель мне кажется
            // если изначально он был неактивным
            _cashCount.EmptyDailyCashAmount();
            _handGenerator.UnblockHandGeneratorAfterWait();
        }
    }
}