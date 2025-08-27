namespace MyCodeBase.Timers
{
    public interface IProgressable : ITimeable, IDurationable
    {
        event System.Action<float> OnProgressChanged;

        float GetProgress();
        void SetProgress(float progress);
    }
}
