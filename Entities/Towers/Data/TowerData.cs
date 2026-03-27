using Godot;
using System;

public enum RarityLevel
{
    Forgotten,
	Whispered,
    Historic,
    Mythic
}

[GlobalClass] 
public partial class TowerData : Resource
{
    [Export] public string TowerName;

    [Export] public RarityLevel Rarity;

    [Export] public int Cost = 10;

	[Export] public SpriteFrames Sprite;

	[Export] public SpriteFrames AttackSprite;

	[Export] public float Range;
    
	[Export] public float FireRate;
    
	[Export] public int Damage;

	[Export] public AttackVisual AttackVisual;
}
