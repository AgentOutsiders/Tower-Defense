using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]
	public PackedScene EnemyScene;

	[Export]
	public Path2D ConnectedPath;

	[Export]
	public float SpawnInterval;

	private Timer _spawnTimer;

	private int c = 0;

    public override void _Ready()
    {
        _spawnTimer = GetNode<Timer>("SpawnTimer");
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.Timeout += SpawnEnemy;
    }

	private void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		ConnectedPath.AddChild(newEnemy);
		c++;
		GD.Print("Spawned enemy " + c);
	}
}
