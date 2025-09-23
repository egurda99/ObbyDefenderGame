using Code.Infrastructure.Services.StaticData.Configs;

namespace Code.Infrastructure.Services.StaticData
{
  public interface IStaticDataService
  {
    LevelStaticData GetLevel(string sceneKey);
    void Load();
    void GetEnemy();
    void GetUi();
  }
}