using Code.Infrastructure.Services.StaticData;

namespace Code.Infrastructure.AppRunner.LoadSteps.BootWarmup
{
  public class BootWarmupStep 
  {
    private readonly IStaticDataService _staticData;

    public BootWarmupStep(IStaticDataService staticData)
    {
      _staticData = staticData;
    }

    public void Warmup()
    {
      _staticData.Load();
    }
  }
}