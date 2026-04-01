using Godot;

[GlobalClass]
public partial class SlowEffect : AttackEffect
{
    [Export] public float SpeedMultiplier = 0.5f; // 0.5 = l'ennemi perd 50% de sa vitesse

    public override ActiveStatusEffect CreateActiveEffect(Enemy target)
    {
        return new ActiveSlowEffect(target, Duration, SpeedMultiplier);
    }
}

public class ActiveSlowEffect : ActiveStatusEffect
{
    private float _speedMultiplier;
    private float _originalSpeed;

    public ActiveSlowEffect(Enemy target, float duration, float multiplier) : base(target, duration)
    {
        _speedMultiplier = multiplier;
    }

    public override void OnApply()
    {
        _originalSpeed = Target.Speed;
        Target.Speed *= _speedMultiplier; // On réduit sa vitesse à l'impact
    }

    public override void OnRemove()
    {
        Target.Speed = _originalSpeed; // On lui redonne sa vitesse normale quand l'effet s'arrête !
    }
}