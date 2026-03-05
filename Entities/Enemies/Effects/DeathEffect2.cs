using Godot;
using System;

[GlobalClass]
public partial class DeathEffect2 : EnemyEffect
{
    public override void OnDeath(Enemy enemy)
    {
        GD.Print("Death 2 effect" + enemy);
    }
}
