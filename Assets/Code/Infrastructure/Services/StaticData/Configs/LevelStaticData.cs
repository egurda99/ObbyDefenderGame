using UnityEngine;

namespace Code.Infrastructure.Services.StaticData.Configs
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Vector3 PlayerSpawnPosition;
  }
}