using Godot;
using System;

public partial class Enemy : PathFollow2D
{
	[Export] public EnemyData Data;
	
	public float Speed = 100.0f;

	private int _health = 100;

	private AnimatedSprite2D _sprite;

	private bool _isDead = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		ApplyData();

		if (Data.Effects != null) // Calls the OnSpawn method for each effect
        {
            foreach (var effect in Data.Effects)
                effect.OnSpawn(this);
        }

		_sprite.Play("default");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;

		if (ProgressRatio >= 1.0f)
		{
			QueueFree();
		}

		if (Data.Effects != null) // Calls the OnProcess method for each effect
        {
            foreach (var effect in Data.Effects)
                effect.OnProcess(this, delta);
        }
	}

	private void ApplyData()
	{
		Speed = Data.Speed;
		_health = Data.Health;
		_sprite.SpriteFrames = Data.Sprite;
	}

	public void TakeDamage(int damage)
	{
		if (_isDead) // If the enemy is already dead but still in the queue to be freed
		{
			return;
		}
			
		_health -= damage;
		if (_health <= 0)
		{
			_isDead = true;
			Die();
		}
	}

	private void Die()
	{
		if (Data.Effects != null) // Calls the OnDeath method for each effect
        {
            foreach (var effect in Data.Effects)
                effect.OnDeath(this);
        }
		QueueFree();
	}
}
