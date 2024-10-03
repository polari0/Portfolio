using Godot;
using System;
using System.Globalization;

/// <summary>
/// Base class for the different player stats 
/// </summary>
public class Stat
{
	public string statName {get; private set;}
	public float statValue {get; private set;}
	public float statUpgradeStep {get; private set;}
	public float statCurrentUpgradeStep {get; private set;}
	public float statMaxUpgrades {get; private set;}
	public float statUpgradeCost {get; private set;}
	public float statBaseUpgradeCost {get; private set;}

	/// <summary>
	/// Initialiser for player stats
	/// </summary>
	/// <param name="_statName">Name of the stat</param>
	/// <param name="_statInitialValue">Stat starting value</param>
	/// <param name="_statUpgradeStep">How much does each upgrade increase the stat?</param>
	/// <param name="_statMaxUpgrades">What is the stat max level</param>
	///	<param name="_statCurrentUpgradeStep">What is the current upgrade value of the stat</param>
	///	<param name="_statCurrentUpgradeStep">How much does upgrading the stat cost?</param>
	public Stat(string _statName, float _statInitialValue, float _statUpgradeStep, float _statMaxUpgrades, float _statCurrentUpgradeStep, float _statUpgradeCost, float _statBaseUpgradeCost)
	{
		statName = _statName;
		statValue = _statInitialValue;
		statUpgradeStep = _statUpgradeStep;
		statMaxUpgrades = _statMaxUpgrades;
		statCurrentUpgradeStep = _statCurrentUpgradeStep;
		statUpgradeCost = _statUpgradeCost;
		statBaseUpgradeCost = _statBaseUpgradeCost;
	}

	public bool CanUpgrade(float currentResourceStack)
	{
		return currentResourceStack >= statUpgradeCost && statCurrentUpgradeStep <= statMaxUpgrades;
	}
	public void Upgrade(float currentResourceStack)
	{
		if(CanUpgrade(currentResourceStack))
		{
			statValue += statUpgradeStep;
			statCurrentUpgradeStep ++;
			statUpgradeCost += statCurrentUpgradeStep * statUpgradeCost;
		}	
	}
}
