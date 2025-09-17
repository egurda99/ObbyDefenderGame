using System;
using System.Collections.Generic;
using MyCodeBase.SaveLoader;

namespace GameEngine
{
    [Serializable]
    public sealed class UpgradesData
    {
        public List<UpgradeData> UpgradesDataList = new();

        public UpgradesData(List<UpgradeData> resourcesDataList)
        {
            if (resourcesDataList != null)
            {
                UpgradesDataList.AddRange(resourcesDataList);
            }
        }
    }
}
