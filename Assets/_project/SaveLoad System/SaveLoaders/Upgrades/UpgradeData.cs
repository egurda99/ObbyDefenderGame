using System;

namespace MyCodeBase.SaveLoader
{
    [Serializable]
    public sealed class UpgradeData
    {
        public string Id;
        public int Level;

        public UpgradeData(string id, int amount)
        {
            Id = id;
            Level = amount;
        }
    }
}
