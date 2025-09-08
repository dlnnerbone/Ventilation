using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Logic;
using GameComponents.Entity;
using System;
using GameComponents;
namespace Main;
public sealed class Bullet : Projectile 
{
    private float bulletSpd = 200;
    private float speedMulti = 1;
    private float dmg = 25;
    private float dmgMulti = 1;
    // private fields
    public float BulletSpeed { get { return bulletSpd; } set { bulletSpd = value * speedMulti; } }
    public float Damage { get { return dmg; } private set { dmg = value * dmgMulti; } }
    public float SpeedMultiplier { get { return speedMulti; } set { speedMulti = Math.Abs(value); } }
    public float DamageMultiplier { get { return dmgMulti; } set { dmgMulti = Math.Abs(value); } }
}