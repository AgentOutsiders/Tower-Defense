using Godot;
using System;

[GlobalClass]
public partial class DeathEffect1 : EnemyEffect
{
    public override void OnDeath(Enemy enemy)
    {
        GD.Print("Death 1 effect" + enemy);
    }
}
