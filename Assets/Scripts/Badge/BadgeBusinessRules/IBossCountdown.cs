namespace BadgeImplementation.BusinessRules
{
    public interface IBossCountdown
    {
        void StopCountdown();
        void StartCountdown(int timer);
    }
}