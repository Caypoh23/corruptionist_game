using UnityEngine;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject progressBar;
    
        public void AnimateBar(float currentTimerValue)
        {
            LeanTween.scaleY(progressBar, 1, currentTimerValue).setOnComplete(() =>
            {
                progressBar.transform.localScale = new Vector3(1.0f, 0.0f, 1.1f);
            });
        }
    }
}
