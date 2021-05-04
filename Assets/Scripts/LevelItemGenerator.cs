using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemGenerator : MonoBehaviour
{
    [SerializeField] private LevelItemStruct[] items;

    private LevelController levelController;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>(); 
    }
    private void Update()
    {
        Debug.Log("Current Level: " +levelController.GetCurrentLevel());
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].startLevel > items[i].endLevel)
            {
                Debug.LogError("StartLevel is > than EndLevel in LevelItemGenerator. We made them equal. Eat Shit");
                items[i].startLevel = items[i].endLevel;
            }
            //if item levels have current level in range
            if (levelController.GetCurrentLevel() >= items[i].startLevel && levelController.GetCurrentLevel() <= items[i].endLevel)
            {
                items[i].itemGO.SetActive(true);
            }
            else
            {
                items[i].itemGO.SetActive(false);
            }

            
        }
    }
}
