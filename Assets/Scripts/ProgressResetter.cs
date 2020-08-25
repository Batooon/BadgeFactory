public class ProgressResetter
{
    private DefaultAutomationsData _defaultAutomationsData;

    public ProgressResetter(DefaultAutomationsData defaultAutomations)
    {
        _defaultAutomationsData = defaultAutomations;
    }

    public void ResetProgress(PlayerData playerData, BadgeData badgeData, AutomationsData automationsData)
    {
        playerData.ResetData();
        badgeData.ResetData();
        automationsData.ResetData();
    }
}
