using Godot;
using System;

[GlobalClass]
public partial class InstantAttackVisual : AttackVisual
{
    public override void Play(Node2D tower, Enemy target, SpriteFrames attackSprite, Action onHit)
    {
        AnimatedSprite2D attackAnimation = new AnimatedSprite2D();
        attackAnimation.SpriteFrames = attackSprite;

        target.AddChild(attackAnimation);

        attackAnimation.AnimationFinished += () =>
        {
            attackAnimation.QueueFree(); // On supprime le visuel
            onHit?.Invoke();       // On déclenche les dégâts
        };

        attackAnimation.Play("default");
    }
}
