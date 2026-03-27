using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] public PackedScene EnemyScene;

	[Export] public Path2D ConnectedPath;

	[Export] public float SpawnInterval;

	private Timer _spawnTimer;

	private AnimatedSprite2D _animatedSprite;

	private int c = 0;

    public override void _Ready()
    {
        _spawnTimer = GetNode<Timer>("SpawnTimer");
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.Timeout += SpawnEnemy;
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite.AnimationFinished += OnAnimationFinished;
    }

	private void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		ConnectedPath.AddChild(newEnemy);
		_animatedSprite.Play("Spawn");

		c++;
		GD.Print("Spawned enemy " + c);
	}

	private void OnAnimationFinished()
    {
        _animatedSprite.Play("default");
    }
}
