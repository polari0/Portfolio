using Godot;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


/// <summary>
/// Player stat manager handles upgrading and initialzing different player stats in the game. 
/// </summary>
public partial class PlayerStatsManager : Node
{
	//Stores the player stats in neat dictionary
	public Dictionary<string, Stat> stats {get; private set;}

	public int presitigeLevel = 1;
	[Export]
	public float resourceStackSize; 

//The fact that I need to even do this seems stupid but oh well 
	PlayerStatsManager playerStatsManager;

	//Initialises the player stats when this script is generated
    public override void _Ready()
    {
		playerStatsManager = this;
        stats = new Dictionary<string, Stat>
		{
			{"playerHealth", new Stat("Health", 100f, 50f, 20f, 1f, 20f, 20f)},
			{"playerSpeed", new Stat("Speed", 100f, 100, 10f, 1f, 20f, 30f)},
			{"playerDamage", new Stat("Damage", 10f, 5f, 50f, 1f, 20f, 20f)},
			{"projectileSpeed", new Stat("ProjectileSpeed", 200f, 50f, 1f, 1f, 30f, 30f)},
			{"playerFireRate", new Stat("FireRate", 0.2f, -0.2f, 4f, 1f, 100f, 100f)},
			{"projectileLifeTime", new Stat("ProjectileLifeTime", 3.0f, 1f, 5f, 1f, 50f, 50f)}
		};
    }

	public void UpgradeStat(string statName)
	{
		if(stats.ContainsKey(statName))
		{
			stats[statName].Upgrade(resourceStackSize);
			GD.Print(stats[statName].statValue);
		}	
	}
	public float GetStatValue(string statName)
	{
		return stats.ContainsKey(statName) ? stats[statName].statValue : 0f;
	}

	public float GetStatUpgradePrice(string statName)
	{
		return stats.ContainsKey(statName) ? stats[statName].statUpgradeCost : 0f;
	}

	public void IncreaseResourceStack()
	{
		resourceStackSize += 1 * presitigeLevel;
	}
}
