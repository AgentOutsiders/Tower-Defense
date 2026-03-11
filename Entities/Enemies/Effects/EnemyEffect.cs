using Godot;
using System;

[GlobalClass]
public abstract partial class EnemyEffect : Resource
{
    public virtual void OnSpawn(Enemy enemy) { }

    public virtual void OnProcess(Enemy enemy, double delta) { }

    public virtual void OnDeath(Enemy enemy) { }
}