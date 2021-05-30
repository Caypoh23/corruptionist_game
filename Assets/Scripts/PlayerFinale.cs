using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinale : MonoBehaviour
{
    [SerializeField] private GameObject burstParticles;
    [SerializeField] private GameObject burstMoney;
    [SerializeField] private GameObject boobles;

    [SerializeField] private Transform burstPoint;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    public void GoBubblesGo()
    {
        Instantiate(burstParticles, burstPoint.transform.position, burstPoint.transform.rotation);
        Instantiate(burstMoney, burstPoint.transform.position, burstPoint.transform.rotation);
        Instantiate(boobles, burstPoint.transform.position, burstPoint.transform.rotation);
    }
    public void PlaySound()
    {
        _audioManager.Play("bigBooble");
        _audioManager.Play("boobles");
    }
}