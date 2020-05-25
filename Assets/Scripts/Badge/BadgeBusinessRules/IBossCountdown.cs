namespace Badge.BusinessRules
{
    public interface IBossCountdown
    {
        void StopCountdown();
        void StartCountdown(int timer);
    }
}