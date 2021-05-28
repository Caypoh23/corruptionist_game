using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePickUp : MonoBehaviour
{
    [SerializeField] private AudioSource pickUpSound;
    [SerializeField] private AudioSource talkSound;




    private bool _isPickedUp;

    private void OnMouseDown()
    {
        _isPickedUp = true;
       
        pickUpSound.Play();
       
            talkSound.Play();
        
    }
    public bool CheckIfPhoneIsPiCkedUp()
    {
        return _isPickedUp;
    }
    public void ResetPhone()
    {
        _isPickedUp = false;
    }
    //TODO If not taken -200$
}
