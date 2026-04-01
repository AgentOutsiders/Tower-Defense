using Godot;
using System;

[GlobalClass]
public abstract partial class AttackEffect : Resource
{
    [Export] public float Duration = 3.0f;

    public abstract ActiveStatusEffect CreateActiveEffect(Enemy target);
}

public abstract class ActiveStatusEffect
{
    public float TimeLeft;
    public Enemy Target;
    public bool IsFinished => TimeLeft <= 0;

    public ActiveStatusEffect(Enemy target, float duration)
    {
        Target = target;
        TimeLeft = duration;
    }

    public virtual void OnApply() { }

    public virtual void OnProcess(double delta) { }

    public virtual void OnRemove() { }
}