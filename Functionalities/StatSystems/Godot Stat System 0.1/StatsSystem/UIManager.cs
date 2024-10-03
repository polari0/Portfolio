using Godot;
using System;

public partial class UIManager : Node
{
	[Export] private PlayerStatsManager playerStatsManager;
	[Export] private Button upgradehealtButton;
	[Export] private Button upgradeSpeedButton;
	[Export] private Button upgradeDamageButton;
	[Export] private Button upgradeProjectileSpeedButton;
	[Export] private Button upgradeFireRateButton;
	[Export] private Button upgradeProjectileLifeTimeButton;

	/*This is starting to seem slight bit stupid but oh well it will work for now. 
	How ever if you try to make stats system again figure out something else than this. 
	It is expandable but takes lot of code writing. 
	*/	
	public override void _Ready()
    {
		upgradehealtButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("playerHealth")));
		upgradeSpeedButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("playerSpeed")));
		upgradeDamageButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("playerDamage")));
		upgradeProjectileSpeedButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("projectileSpeed")));
		upgradeFireRateButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("playerFireRate")));
		upgradeProjectileLifeTimeButton.Connect("pressed", Callable.From(() => OnUpgradeButtonPressed("projectileLifeTime")));
		UpdateUI();
    }
    private void OnUpgradeButtonPressed(string statName)
	{
		if(playerStatsManager.resourceStackSize >= playerStatsManager.stats[statName].statUpgradeCost)
		{
			playerStatsManager.UpgradeStat(statName);
			UpdateUI();
		}
	}

	private void UpdateUI()
	{
		upgradehealtButton.Text = "Upgrade Health\n" + playerStatsManager.GetStatUpgradePrice("playerHealth").ToString();
		upgradeSpeedButton.Text = "Upgrade Speed\n" + playerStatsManager.GetStatUpgradePrice("playerSpeed").ToString();
		upgradeDamageButton.Text = "Upgrade Damage\n" + playerStatsManager.GetStatUpgradePrice("playerDamage").ToString();
		upgradeProjectileSpeedButton.Text = "Upgrade Projectile Speed\n" + playerStatsManager.GetStatUpgradePrice("projectileSpeed").ToString();
		upgradeFireRateButton.Text = "Upgrade Fire Rate\n" + playerStatsManager.GetStatUpgradePrice("playerFireRate").ToString();
		upgradeProjectileLifeTimeButton.Text = "Upgrade Projectile Life Time\n" + playerStatsManager.GetStatUpgradePrice("projectileLifeTime").ToString();
	}
}
