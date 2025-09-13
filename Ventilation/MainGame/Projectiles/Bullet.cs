using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Rendering;
using GameComponents.Logic;
using GameComponents.Entity;
using System;
using GameComponents;
namespace Main;
public sealed class Bullet : Projectile 
{
    private float moveSpeed = 250f;
    private float speedMulti = 1;
    private float dmg = 20;
    private float dmgMulti = 1;
    // private variables
    public Sprite BulletTexture { get; private set; }
    public Rectangle HitBox { get; set; }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value * speedMulti; } }
    public float SpeedMultiplier { get { return speedMulti; } set { speedMulti = Math.Abs(value); } }
    public float Damage { get { return dmg; } set { dmg = Math.Abs(value); } }
    public float DamageMultiplier { get { return dmgMulti; } set { dmgMulti = Math.Abs(value); } }
    // methods.
}