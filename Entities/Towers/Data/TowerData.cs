using Godot;
using System;

[GlobalClass] 
public abstract partial class TowerData : Resource
{
	[Export]
	public Texture2D Sprite;

	[Export]
	public float Range;
    
	[Export]
	public float FireRate;
    
	[Export]
	public int Damage;
}
