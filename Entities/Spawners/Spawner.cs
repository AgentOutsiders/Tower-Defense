using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]
	public PackedScene EnemyScene;

	[Export]
	public Path2D ConnectedPath;

	[Export]
	public float SpawnInterval = 2.0f;

	private float _timer = 0.0f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_timer += (float)delta;
		if (_timer >= SpawnInterval)
		{
			_timer = 0.0f;
			SpawnEnemy();
		}
	}

	private void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		ConnectedPath.AddChild(newEnemy);
	}
}
