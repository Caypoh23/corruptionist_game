using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessCashProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private Image animatedPart;

    [SerializeField] private EndlessModeController endlessModeController;
    [SerializeField] private float coef = 0.2f;
    private float value;
    private float maxValue;

    private bool _canDecrement;

    private void Awake()
    {
        _canDecrement = false;
    }
    private void FixedUpdate()
    {
        if (value != 0 && _canDecrement)
        {
            value -= coef * Time.deltaTime;
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animatedPart.color = gradient.Evaluate(slider.normalizedValue);
        }
        else
        {
            _canDecrement = false;
            Debug.Log("Lose");
        }
        
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

    public void SetValue(float cash)
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

   public void StartDecrementValue()
    {
        _canDecrement = true;
    }
}
