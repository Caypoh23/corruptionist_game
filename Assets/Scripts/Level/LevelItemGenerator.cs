using UnityEngine;

namespace Level
{
    public class LevelItemGenerator : MonoBehaviour
    {
        [SerializeField] private LevelItemStruct[] items;
        [SerializeField] private LevelPlayerStruct[] players;

        private LevelController levelController;


        private void Awake()
        {
            levelController = FindObjectOfType<LevelController>();
        }

        private void Start()
        {
            LoadItems();
        }

        private void Update()
        {
           // Debug.Log("Current Level: " + levelController.currentLevel);
        }

        public void LoadItems()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].startLevel > items[i].endLevel)
                {
                    Debug.LogError("StartLevel is > than EndLevel for " + items [i].name+ " in LevelItemGenerator. We made them equal. Eat Shit");
                    items[i].startLevel = items[i].endLevel;
                }

                //if item levels have current level in range
                if (levelController.currentLevel >= items[i].startLevel &&
                    levelController.currentLevel <= items[i].endLevel)
                {
                    items[i].itemGO.SetActive(true);
                }
                else
                {
                    items[i].itemGO.SetActive(false);
                }
            }

            // chnaging players
            for (int j = 0; j < players.Length; j++)
            {
                if (players[j].level == levelController.currentLevel)
                {
                    players[j].playerGO.SetActive(true);
                }
                else
                {
                    players[j].playerGO.SetActive(false);
                }
            }
        }
    }
}