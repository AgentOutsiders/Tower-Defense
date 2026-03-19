using Godot;
using System;

[GlobalClass]
public abstract partial class AttackVisual : Resource
{
    public virtual void Play(Node2D tower, Enemy target, SpriteFrames attackSprite, Action onHit)
    {
        
    }
}
