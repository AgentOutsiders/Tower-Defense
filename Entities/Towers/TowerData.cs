using Godot;

[GlobalClass] 
public partial class TowerData : Resource
{
	[Export]
	public Texture2D Sprite;

	[Export]
	public float Range = 100.0f;
    
	[Export]
	public float FireRate = 1.0f;
    
	[Export]
	public int Damage = 10;
}
