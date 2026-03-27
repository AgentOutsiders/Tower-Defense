using Godot;
using System;

public partial class ShopManager : CanvasLayer
{

	[Export] public Godot.Collections.Array<TowerData> ForgottenTowers = new Godot.Collections.Array<TowerData>();
    [Export] public Godot.Collections.Array<TowerData> WhisperedTowers = new Godot.Collections.Array<TowerData>();
    [Export] public Godot.Collections.Array<TowerData> HistoricTowers = new Godot.Collections.Array<TowerData>();
    [Export] public Godot.Collections.Array<TowerData> MythicTowers = new Godot.Collections.Array<TowerData>();

	[Export] public int ForgottenChance = 70;
    [Export] public int WhisperedChance = 20;
    [Export] public int HistoricChance = 8;
    [Export] public int MythicChance = 2;

	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng.Randomize();

		RollShop(4);
	}

	public void RollShop(int n)
	{
		GD.Print("--- New Shop Roll ---");
        
        for (int i = 0; i < 4; i++)
        {
            TowerData drawnTower = GetRandomTower();
            if (drawnTower != null)
            {
                GD.Print($"Slot {i + 1} : {drawnTower.TowerName} (Price: {drawnTower.Cost} - Rarity: {drawnTower.Rarity})");
                
            }
        }
        GD.Print("-------------------------------------");
	}

	private TowerData GetRandomTower()
	{
		int roll = _rng.RandiRange(1, 100);
        RarityLevel rolledRarity;

        if (roll <= ForgottenChance) 
            rolledRarity = RarityLevel.Forgotten;
        else if (roll <= ForgottenChance + WhisperedChance) 
            rolledRarity = RarityLevel.Whispered;
        else if (roll <= ForgottenChance + WhisperedChance + HistoricChance) 
            rolledRarity = RarityLevel.Historic;
        else 
            rolledRarity = RarityLevel.Mythic;

		Godot.Collections.Array<TowerData> targetList = GetListForRarity(rolledRarity);

		int randomIndex = _rng.RandiRange(0, targetList.Count - 1);
        return targetList[randomIndex];
	}

	private Godot.Collections.Array<TowerData> GetListForRarity(RarityLevel rarity)
    {
        switch (rarity)
        {
            case RarityLevel.Forgotten: return ForgottenTowers;
            case RarityLevel.Whispered: return WhisperedTowers;
            case RarityLevel.Historic: return HistoricTowers;
            case RarityLevel.Mythic: return MythicTowers;
            default: return null;
        }
    }
}
