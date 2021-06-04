using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    // Start is called before the first frame update
    public void DeleteAllData()
    {
        DataManager.Instance.DeleteCashState();
        DataManager.Instance.DeleteLevelNumber();
        DataManager.Instance.DeleteLostMoney();
        DataManager.Instance.DeletePoliceCaughtNumber();
    }
}
