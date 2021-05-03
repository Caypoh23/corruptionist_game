using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashCount : MonoBehaviour
{
    [SerializeField] private TMP_Text cashCountText;

    private float cashCount;
    //TODO: mb event
    // Start is called before the first frame update
    void Start()
    {
        cashCountText.SetText("0");
    }

    public void addCash(float amount)
    {
        StartCoroutine(Pulse(amount));
    }
    private IEnumerator Pulse(float amount)
    {
        for (float i = 1f; i < 1.2f; i += 0.05f)
        {
            cashCountText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        cashCountText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        cashCount += amount;
        cashCountText.SetText(cashCount.ToString());

        for (float i = 1.2f; i >= 1f ; i-= 0.05f)
        {
            cashCountText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        cashCountText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public float GetEarnedCash()
    {
        return cashCount;
    }
}
