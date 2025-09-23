using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services.StaticData.Configs;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<string, LevelStaticData> _levels = new();

    public StaticDataService()
    {
      Debug.Log("StaticDataService initialized");
      Load();
      foreach (KeyValuePair<string, LevelStaticData> kvp in _levels)
      {
        Debug.Log(kvp.Value.LevelKey);
      }
    }

    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>("Resources/StaticData")
        .ToDictionary(data => data.LevelKey, data => data);
    }
    
    public LevelStaticData GetLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;

    public void GetEnemy()
    {
      
    }

    public void GetUi()
    {
      
    }
  }
}