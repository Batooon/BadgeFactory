public class ProgressResetter
{
    private PlayerData _playerData;
    private BadgeData _badgeData;
    private AutomationsData _automationsData;

    public ProgressResetter(PlayerData playerData,
                            BadgeData badgeData,
                            AutomationsData automationsData)
    {
        _playerData = playerData;
        _badgeData = badgeData;
        _automationsData = automationsData;
    }

    public void ResetProgress()
    {
        _playerData = new PlayerData();
        _badgeData = new BadgeData();
        _automationsData = new AutomationsData();

        /*
        _playerData.BossCountdownTime = 30;
        _playerData.Gold = 0;
        _playerData.Level = 1;
        _playerData.LevelProgress = 0;
        _playerData.MaxLevelProgress = 10;

        _badgeData.CoinsReward = 1;
        _badgeData.CurrentHp = 0;
        _badgeData.MaxHp = 10;

        _automationsData.AutomationsPower = 0;
        _automationsData.ClickPower = 1;
        _automationsData.CanUpgradeSomething = false;
        _automationsData.LevelsToUpgrade = 1;

        for (int i = 0; i < _automationsData.Automations.Count; i++)
        {
            Automation automationData = _automationsData.Automations[i];
            automationData.CanUpgrade = false;
            automationData.CurrentCost = automationData.StartingCost;
            automationData.CurrentDamage = automationData.StartingDamage;
            automationData.Level = i == 0 ? 1 : 0;
            automationData.IsUnlocked = i == 0;
        }*/
    }
}
