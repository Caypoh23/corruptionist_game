using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CashProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;
        [SerializeField] private Image animatedPart;

        private LevelController levelController;
        private float value;
        private float maxValue;
        private float maxCashBarPerLevel;

        private void Awake()
        {
            levelController = FindObjectOfType<LevelController>();
        }

        public void SetMaxValue(float cash)
        {
            maxValue = cash;
            slider.maxValue = cash;
            value = 0;
            slider.value = value;

            fill.color = gradient.Evaluate(0f);
            animatedPart.color = gradient.Evaluate(0f);
        }

        public void SetMaxValuePerLevel(float cash)
        {
            maxCashBarPerLevel = cash;
        }
        public void FillMaxValuePerLevel(float cash)
        {
            value = cash;
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animatedPart.color = gradient.Evaluate(slider.normalizedValue);
        }
        public void AddValue(float cash)
        {
            var prevValue = value;
            value += cash;

            if (value > maxCashBarPerLevel * levelController.currentLevel)
            {
                value = prevValue;
                slider.value = prevValue;
                if (levelController.currentLevel == 7)
                {
                    slider.value = maxValue; // set progressbar full on 7 level
                }
            }
            else
            {
                slider.value = value;
            }
           
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animatedPart.color = gradient.Evaluate(slider.normalizedValue);
        }
        public void SetValueForLevel(int level)
        {
            value = maxCashBarPerLevel * levelController.currentLevel - maxCashBarPerLevel;
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animatedPart.color = gradient.Evaluate(slider.normalizedValue);
        }
        public void RemoveValue(float cash)
        {
            var prevValue = value;
            value -= cash;

            if (value < 0)
            {
                value = 0;
            }

            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animatedPart.color = gradient.Evaluate(slider.normalizedValue);
        }

        public float GetValue()
        {
            return slider.value;
        }
    }
}