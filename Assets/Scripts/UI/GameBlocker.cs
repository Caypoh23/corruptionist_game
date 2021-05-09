using System;
using System.Collections;
using System.Collections.Generic;
using Hand;
using UnityEngine;

public class GameBlocker : MonoBehaviour
{
    private HandGenerator handGenerator;

    private void Awake()
    {
        handGenerator = FindObjectOfType<HandGenerator>();
    }

    public void ShakeCamera()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 5f, 0.1f, 1.0f);
    }

    public void SetActiveFalse()
    {
        handGenerator.UnblockHandGenerator();
        gameObject.SetActive(false);
    }
}