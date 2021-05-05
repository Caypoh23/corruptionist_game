using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlocker : MonoBehaviour
{
    public void ShakeCamera()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 5f, 0.1f, 1.0f);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}