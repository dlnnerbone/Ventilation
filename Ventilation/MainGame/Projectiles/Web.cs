using System;
using GameComponents;
using GameComponents.Rendering;
using GameComponents.Entity;
using Microsoft.Xna.Framework;
using GameComponents.Logic;
namespace Main;
public sealed class WebClump : Projectile 
{
    //
    private Timer _lifeSpan; 
    private float _moveSpeed = 200f;
    private float _maxSpeed = 500f;
    private float _speedMulti = 1f;
    private float _damage = 25f;
    private float _damageMulti = 1f;
    //
    
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value * _speedMulti; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = MathHelper.Clamp(value, _moveSpeed, float.PositiveInfinity) * _speedMulti; }
    public float SpeedMulti { get => _speedMulti; set => _speedMulti = Math.Abs(value); }
    public float Damage { get => _damage; set => _damage = value * _damageMulti; }
    public float DamageMulti { get => _damageMulti; set => _damageMulti = Math.Abs(value); }
    
    public WebClump(int x = 0, int y = 0, int width = 32, int height = 32) : base(x, y, width, height, Vector2.UnitX) 
    {
        _lifeSpan = new(5f, TimeStates.Down, false, true);
    }
    
}