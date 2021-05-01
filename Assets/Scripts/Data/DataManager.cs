using UnityEngine;

namespace Data
{
    public class DataManager : SingletonClass<DataManager>
    {
        private const string CoinsConstant = "Cash";

        public int Cash { get; private set; }

        public override void Awake()
        {
            base.Awake();
            LoadAllData();
        }

        public void SaveCoinState(int cashCount) => ES3.Save(CoinsConstant, Cash = cashCount);

        public int LoadCash()
        {
            if (ES3.KeyExists(CoinsConstant))
            {
                return Cash = ES3.Load<int>(CoinsConstant);
            }

            return 0;
        }

        public void LoadAllData()
        {
            LoadCash();
        }
    }
}