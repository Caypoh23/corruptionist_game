using UnityEngine;

namespace Data
{
    public class DataManager : SingletonClass<DataManager>
    {
        private const string CashConstant = "Cash";
        private const string LaunchCountConstant = "LaunchCount";
        private const string LevelConstant = "LevelNumber";

        public int Cash { get; private set; }
        public int LaunchCount { get; private set; }
        public int LevelNumber { get; set; } = 1;

        public override void Awake()
        {
            base.Awake();
            LoadAllData();
        }

        public void SaveCashState(int cashCount) => ES3.Save(CashConstant, Cash = cashCount);

        public void SaveLaunchCount(int launchCount) => ES3.Save(LaunchCountConstant, LaunchCount = launchCount);

        public void SaveLevelNumber(int levelNumber) => ES3.Save(LevelConstant, LevelNumber = levelNumber);


        // can be made as 1 method

        #region Load Saved Value

        public int LoadCash()
        {
            if (ES3.KeyExists(CashConstant))
            {
                return Cash = ES3.Load<int>(CashConstant);
            }

            return 0;
        }

        public int LoadLaunchNumber()
        {
            if (ES3.KeyExists(LaunchCountConstant))
            {
                return LaunchCount = ES3.Load<int>(LaunchCountConstant);
            }

            return 0;
        }

        public int LoadLevelNumber()
        {
            if (ES3.KeyExists(LevelConstant))
            {
                return LevelNumber = ES3.Load<int>(LevelConstant);
            }

            return 1;
        }

        #endregion

        public void LoadAllData()
        {
            LoadCash();
            LoadLaunchNumber();
            LoadLevelNumber();
        }

        public void DeleteLevelNumber()
        {
            if (ES3.KeyExists(LevelConstant))
            {
                ES3.DeleteKey(LevelConstant);
            }
        }

        public void DeleteCashState()
        {
            if (ES3.KeyExists(CashConstant))
            {
                ES3.DeleteKey(CashConstant);
            }
        }
    }
}