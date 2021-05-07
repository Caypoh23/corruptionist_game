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
        [SerializeField] private TMP_Text caughtTimesText;
        private Animator panelAnimator;

        public Action<int, float, int> OnShowPanel;

        private HandGenerator handGenerator;
       

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
            handGenerator = FindObjectOfType<HandGenerator>();
           
        }

        private void Start()
        {
            levelPanel.SetActive(false);
        }

        public void ShowPanel(int dayNumber, float cashEarned, int caughtTimes)
        {
            dayNumberText.SetText("День " + dayNumber.ToString() + " завершен");
            cashEarnedText.SetText("Заработанно: " + cashEarned.ToString());
            caughtTimesText.SetText("Пойман с поличным: " + caughtTimes.ToString());
            levelPanel.SetActive(true);
            panelAnimator.SetBool(Hide, false);
            handGenerator.BlockHandGenerator();
           
        }

        //call from inspector (button event)
        public void NextLevel()
        {
            panelAnimator.SetBool(Hide, true);
            handGenerator.UnblockHandGeneratorAfterWait();
            
        }
    }
}