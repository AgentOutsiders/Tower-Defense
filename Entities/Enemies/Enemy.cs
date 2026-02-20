using Godot;
using System;

public partial class Enemy : PathFollow2D
{
	[Export]
	public float speed = 100.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += speed * (float)delta;

		if (ProgressRatio >= 1.0f)
		{
			QueueFree();
		}
	}
}
