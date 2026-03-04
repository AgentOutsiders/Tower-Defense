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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		ApplyData();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;

		if (ProgressRatio >= 1.0f)
		{
			QueueFree();
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
		Health -= damage;
		if (Health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GD.Print("Enemy died: " + this);
		QueueFree();
	}


}
