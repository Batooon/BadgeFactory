public class ProgressResetter
{
    private DefaultAutomationsData _defaultAutomationsData;

    public ProgressResetter(DefaultAutomationsData defaultAutomations)
    {
        _defaultAutomationsData = defaultAutomations;
    }

    public void ResetProgress(PlayerData playerData, BadgeData badgeData, AutomationsData automationsData)
    {
        playerData = new PlayerData();
        playerData.FireAllChangedEvents();
        badgeData = new BadgeData();
        badgeData.FireAllChangedEvents();
        automationsData = new AutomationsData();
        automationsData.FireAllChangedEvents();
    }
}
