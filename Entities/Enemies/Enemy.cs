using Godot;
using System;

public partial class Enemy : PathFollow2D
{
	[Export]
	public float Speed = 100.0f;

	[Export]
	public int Health = 100;

	[Export]
	public EnemyData Data;

	private Sprite2D _sprite;

	private bool _isDead = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		ApplyData();
		if (Data.Effects != null)
        {
            foreach (var effect in Data.Effects)
                effect.OnSpawn(this);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;

		if (ProgressRatio >= 1.0f)
		{
			QueueFree();
		}

		if (Data.Effects != null)
        {
            foreach (var effect in Data.Effects)
                effect.OnProcess(this, delta);
        }
	}

	private void ApplyData()
	{
		Speed = Data.Speed;
		Health = Data.Health;
		_sprite.Texture = Data.Sprite;
	}

	public void TakeDamage(int damage)
	{
		if (_isDead)
		{
			return;
		}
			
		Health -= damage;
		if (Health <= 0)
		{
			_isDead = true;
			Die();
		}
	}

	private void Die()
	{
		if (Data.Effects != null)
        {
            foreach (var effect in Data.Effects)
                effect.OnDeath(this);
        }
		GD.Print("Enemy died: " + this);
		QueueFree();
	}
}
