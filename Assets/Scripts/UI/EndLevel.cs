using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        [SerializeField] private TMP_Text dayNumberText;
        [SerializeField] private TMP_Text cashEarnedText;
        private Animator panelAnimator;

        public Action<int, float> OnShowPanel;

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
            panelAnimator = panel.GetComponent<Animator>();
        }

        private void Start()
        {
            panel.SetActive(false);
        }

        private void Update()
        {
        }

        public void ShowPanel(int dayNumber, float cashEarned)
        {
            dayNumberText.SetText("День " + dayNumber.ToString() + " завершен");
            cashEarnedText.SetText("Заработанно: " + cashEarned.ToString());
            panel.SetActive(true);
            panelAnimator.SetBool(Hide, false);
        }

        //call from inspector (button event)
        public void NextLevel()
        {
            panelAnimator.SetBool(Hide, true);
        }
    }
}