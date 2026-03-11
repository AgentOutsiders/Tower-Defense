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
	private Sprite2D _sprite;

	private List<Area2D> _enemiesInRange = new List<Area2D>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
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
		_sprite.Texture = Data.Sprite;
		_fireTimer.WaitTime = Data.FireRate;
		_damage = Data.Damage;
	}

	private void OnEnemyEntered(Area2D area)
	{
		if (area.GetParent() is Enemy)
		{
			_enemiesInRange.Add(area);
		}
	}

	private void OnEnemyExited(Area2D area)
    {
        if (_enemiesInRange.Contains(area))
        {
            _enemiesInRange.Remove(area);
        }
    }

	private void OnFireTimerTimeout()
	{
		_enemiesInRange.RemoveAll(enemy => !IsInstanceValid(enemy) || enemy.IsQueuedForDeletion()); // If the enemy is no longer valid or queued for deletion, remove it from the list

		if (_enemiesInRange.Count > 0)
		{
			// Initialize var to sort the enemies in progress order
			Area2D target = _enemiesInRange[0];
			float maxProgress = _enemiesInRange[0].GetParent<Enemy>().ProgressRatio;

			// Find the enemy with the highest progress ratio
			foreach (Area2D area in _enemiesInRange)
			{
				Enemy enemy = area.GetParent<Enemy>();

				if (enemy.ProgressRatio > maxProgress)
				{
					maxProgress = enemy.ProgressRatio;
					target = area;
				}
			}
			Shoot(target);
		}
	}

	private void Shoot(Area2D target)
	{
		target.GetParent<Enemy>().TakeDamage(_damage);
	}
}
