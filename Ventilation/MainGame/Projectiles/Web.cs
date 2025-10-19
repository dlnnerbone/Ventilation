using System;
using GameComponents.Managers;
using GameComponents.Helpers;
using GameComponents.Rendering;
using GameComponents.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Logic;
namespace Main;
public sealed class WebClump : Projectile 
{
    // private fields
    private Timer _lifeSpan; 
    private float _moveSpeed = 200f;
    private float _maxSpeed = 500f;
    private float _speedMulti = 1f;
    private float _damage = 25f;
    private float _damageMulti = 1f;
    private float _distance = 32f; 
    // public properties
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value * _speedMulti; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = MathHelper.Clamp(value, _moveSpeed, float.PositiveInfinity) * _speedMulti; }
    public float SpeedMulti { get => _speedMulti; set => _speedMulti = Math.Abs(value); }
    public float Damage { get => _damage; set => _damage = value * _damageMulti; }
    public float DamageMulti { get => _damageMulti; set => _damageMulti = Math.Abs(value); }
    public Sprite WebTexture { get; private set; }
    
    public WebClump(int x = 0, int y = 0, int width = 32, int height = 32) : base(x, y, width, height, Vector2.UnitX) 
    {
        _lifeSpan = new(5f, TimeStates.Down, false, true);
    }
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        WebTexture = new Sprite(new Texture2D(device, 1, 1));
        var _colors = new Color[] { WebTexture.Color };
        WebTexture.Texture.SetData(_colors);
    }
    // state methods
    private void readyState(Entity owner) 
    {
        Direction = MouseManager.WorldMousePosition - Position;
        Position = owner.Center - HalfSize + (Direction * _distance) + ShakeHelper.LinearShake(25f, 1);
    }
    private void stateManager(Entity owner)
    {
        readyState(owner);
    }
    // main Update Method
    public void ShootingTime(GameTime gt, Entity owner) 
    {
        _lifeSpan.TickTock(gt);
        if (_lifeSpan.TimeHitsFloor()) return;
        ShootingTime();
        stateManager(owner);
    }
    public void DrawProjectile(SpriteBatch batch) 
    {
        WebTexture.Draw(batch, Bounds);
    }
}