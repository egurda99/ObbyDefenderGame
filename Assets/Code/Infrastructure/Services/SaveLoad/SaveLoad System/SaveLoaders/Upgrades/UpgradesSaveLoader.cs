using System;
using System.Collections.Generic;
using System.Linq;
using _UpgradePractice.Scripts;
using GameEngine;
using MyCodeBase.SaveLoader;
using UnityEngine;

[Serializable]
public sealed class UpgradesSaveLoader : SaveLoader<UpgradesManager, UpgradesData>
{
    protected override UpgradesData ConvertToData(UpgradesManager service)
    {
        Debug.Log("<color=yellow>Converted to data upgrades</color>");

        var upgradesData = new List<UpgradeData>();

        var upgrades = service.GetAllUpgrades();
        for (var index = 0; index < upgrades.Length; index++)
        {
            var upgrade = upgrades[index];
            upgradesData.Add(new UpgradeData(upgrade.Id, upgrade.Level));
        }

        return new UpgradesData(upgradesData);
    }

    protected override void SetupData(UpgradesManager service, UpgradesData data)
    {
        Debug.Log("<color=yellow>Setuped upgrades data</color>");

        var upgrades = new Upgrade[service.GetAllUpgrades().Length];

        Array.Copy(service.GetAllUpgrades(), upgrades, service.GetAllUpgrades().Length);

        var upgradeDataDictionary = data.UpgradesDataList.ToDictionary(data => data.Id);

        foreach (var upgrade in upgrades)
        {
            if (upgradeDataDictionary.TryGetValue(upgrade.Id, out var upgradeData))
            {
                upgrade.SetLevel(upgradeData.Level);
            }
        }
    }

    protected override void SetupDefaultData(UpgradesManager service)
    {
        Debug.Log("<color=yellow>Setup default data</color>");
    }
}
