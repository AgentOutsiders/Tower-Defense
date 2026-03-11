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

	private Timer _spawnTimer;


    public override void _Ready()
    {
        _spawnTimer = GetNode<Timer>("SpawnTimer");
		_spawnTimer.Timeout += SpawnEnemy;
    }

	private void SpawnEnemy()
	{
		Enemy newEnemy = EnemyScene.Instantiate<Enemy>();
		ConnectedPath.AddChild(newEnemy);
	}
}
