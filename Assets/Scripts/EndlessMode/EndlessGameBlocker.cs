using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessGameBlocker : MonoBehaviour
{
    [SerializeField] private EndlessGameOver gameOver;

    public void ShakeCamera()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 5f, 0.1f, 1.0f);
    }

    // a method to unblock endless hand generator.
    // should have been called from the animation event
    // also plays jail up animation
    public void EndGame()
    {
        gameOver.EndGame("Police Officer arrested you");

       // gameObject.SetActive(false);
    }
}