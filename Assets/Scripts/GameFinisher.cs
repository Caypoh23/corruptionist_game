using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator player;

    public void BurstPlayer()
    {
        Debug.Log("BOOOOM");
        //player.SetTrigger("blowUp");
        
    }
}
