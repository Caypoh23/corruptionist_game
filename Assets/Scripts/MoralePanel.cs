using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoralePanel : MonoBehaviour
{
  
    [SerializeField] private TMP_Text moraleTextField;
    [SerializeField] private string moraleText;

    private void OnEnable()
    {
        StartCoroutine(TypeSentence(moraleText));
    }
    private IEnumerator TypeSentence(string sentence)
    {
        moraleTextField.SetText("");
        foreach (char letter in sentence.ToCharArray())
        {
            moraleTextField.text += letter;
            yield return null;
        }

    }

}
