using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.Services.StaticData.Configs;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
  public class GameFactory : IHeroFactory
  {
    private readonly IStaticDataService _staticData;

    public GameFactory(IStaticDataService staticData)
    {
      _staticData = staticData;
    }

    public void CreateHero(Vector3 position)
    {
      LevelStaticData levelData = _staticData.GetLevel("Level 1");
      GameObject.Instantiate(levelData.PlayerPrefab, levelData.PlayerSpawnPosition, Quaternion.identity);
    }

    public void CreateEnemy(Vector3 position)
    {
      _staticData.GetEnemy();
    }

    public void CreateUI()
    {
      _staticData.GetUi();
    }
  }
}