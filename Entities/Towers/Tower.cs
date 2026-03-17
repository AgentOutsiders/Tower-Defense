using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : Node2D
{

	[Export]
	public TowerData Data;

	private int _damage;
	private Timer _fireTimer;
    private Area2D _attackRangeArea;
	private CollisionShape2D _attackRangeShape;
	private AnimatedSprite2D _sprite;

	private List<Enemy> _enemiesInRange = new List<Enemy>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_fireTimer = GetNode<Timer>("FireTimer");
		_attackRangeArea = GetNode<Area2D>("AttackRange");
		_attackRangeShape = _attackRangeArea.GetNode<CollisionShape2D>("CollisionShape2D");

		ApplyData();

		_fireTimer.Timeout += OnFireTimerTimeout;
		_fireTimer.Start();

		_attackRangeArea.AreaEntered += OnEnemyEntered;
        _attackRangeArea.AreaExited += OnEnemyExited;
	}


	private void ApplyData()
	{
		GD.Print("Applying tower data: " + Data);
		if (_attackRangeShape.Shape is CircleShape2D circleShape)
		{
			circleShape.Radius = Data.Range;
		}
		_sprite.SpriteFrames = Data.Sprite;
		_fireTimer.WaitTime = Data.FireRate;
		_damage = Data.Damage;
	}

	private void OnEnemyEntered(Area2D area)
	{
		if (area.GetParent() is Enemy enemy)
		{
			_enemiesInRange.Add(enemy);
		}
	}

	private void OnEnemyExited(Area2D area)
	{
		if (area.GetParent() is Enemy enemy)
		{
			_enemiesInRange.Remove(enemy);
		}
	}

	private void OnFireTimerTimeout()
	{
		_enemiesInRange.RemoveAll(enemy => !IsInstanceValid(enemy) || enemy.IsQueuedForDeletion()); // If the enemy is no longer valid or queued for deletion, remove it from the list

		if (_enemiesInRange.Count > 0)
		{
			// Initialize var to find the enemies in progress order
			Enemy target = _enemiesInRange[0];
			float maxProgress = target.ProgressRatio;

			// Find the enemy with the highest progress ratio
			foreach (Enemy enemy in _enemiesInRange)
			{
				if (enemy.ProgressRatio > maxProgress)
				{
					maxProgress = enemy.ProgressRatio;
					target = enemy;
				}
			}
			Shoot(target);
		}
	}

	private void Shoot(Enemy target)
	{
		target.TakeDamage(_damage);
	}
}
