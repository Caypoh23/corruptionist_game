using TMPro;
using UnityEngine;

namespace Cash
{
    public class CashText : MonoBehaviour
    {
        public TextMeshPro cashTextUI;

        // Set a prefab object to false in animator 
        private void SetActive()
        {
            gameObject.SetActive(false);
        }
    }
}
