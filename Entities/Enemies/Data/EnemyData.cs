using Godot;
using System;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export]
	public Texture2D Sprite;

    [Export]
    public float Speed = 100.0f;

    [Export]
    public int Health = 100;
    
    [Export]
    public Godot.Collections.Array<EnemyEffect> Effects = new Godot.Collections.Array<EnemyEffect>();
}
