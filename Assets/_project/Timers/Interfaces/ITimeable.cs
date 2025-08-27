namespace MyCodeBase.Timers
{
    public interface ITimeable
    {
        event System.Action<float> OnCurrentTimeChanged;

        float GetCurrentTime();
        void SetCurrentTime(float time);
    }
}
