using ShootEmUp;
using Zenject;

public sealed class SceneContainerUpdater : IGameInitListener
{
    private readonly DiContainer _container;
    private readonly SaveLoadManager _saveLoadManager;

    public SceneContainerUpdater(DiContainer container, SaveLoadManager saveLoadManager)
    {
        _container = container;
        _saveLoadManager = saveLoadManager;
    }

    public void OnInitGame()
    {
        _saveLoadManager.InitOnNewScene(_container);
    }
}
