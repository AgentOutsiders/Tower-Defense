using Godot;
using System;

[GlobalClass] 
public partial class TowerData : Resource
{
	[Export]
	public SpriteFrames Sprite;

	[Export]
	public SpriteFrames AttackSprite;

	[Export]
	public float Range;
    
	[Export]
	public float FireRate;
    
	[Export]
	public int Damage;

	[Export]
	public AttackVisual AttackVisual;
}
