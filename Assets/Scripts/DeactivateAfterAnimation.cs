using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterAnimation : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}