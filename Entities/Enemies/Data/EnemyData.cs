using Godot;
using System;

[GlobalClass]
public partial class EnemyData : Resource
{
	[Export] public SpriteFrames Sprite;

    [Export] public float Speed = 100.0f;

    [Export] public int Health = 100;
    
    [Export] public EnemyEffect[] Effects;
}