using System;
using GameComponents;
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
    private float _moveSpeed = 500f;
    private float _maxSpeed = 500f;
    private float _speedMulti = 1f;
    private float _damage = 25f;
    private float _damageMulti = 1f;
    private float radius = 48f;
    private float distance = 1f;
    private float maxRadius = 48f;
    // public properties
    public Vector2 Target { get; set; } = Vector2.Zero;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value * _speedMulti; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = MathHelper.Clamp(value, _moveSpeed, float.PositiveInfinity) * _speedMulti; }
    public float SpeedMulti { get => _speedMulti; set => _speedMulti = Math.Abs(value); }
    public float Damage { get => _damage; set => _damage = value * _damageMulti; }
    public float DamageMulti { get => _damageMulti; set => _damageMulti = Math.Abs(value); }
    public float Radius { get => radius; set => radius = MathHelper.Clamp(value, 0f, MaxRadius); }
    public float MaxRadius { get => maxRadius; set => maxRadius = Math.Abs(value); }
    
    public TextureAtlas Atlas { get; private set; }
    public Animation WebAnimation { get; set; }
    
    public float Distance => distance > 0 ? Vector2.Distance(Center, Target) : 1f;
    
    public WebClump(int x = 0, int y = 0, int width = 16 * 3, int height = 16 * 3) : base(x, y, width, height, Vector2.UnitX) 
    {
        _lifeSpan = new(5f, TimeStates.Down, false, true);
    }
    public void LoadContent(ContentManager content) 
    {
        Atlas = new TextureAtlas(3, 2, 48, 32);
        WebAnimation = new(content.Load<Texture2D>("GameAssets/ProjectileTextures/WebClump_Active"), Atlas, 0, 5);
        WebAnimation.FPS = 5;
        
        WebAnimation.FPS = 5f;
    }
    // state methods
    private void ready(Entity owner) 
    {
        AimAt(MouseManager.WorldMousePosition);
        Position = owner.Center - HalfSize + Direction * Radius;
    }
    private void active(GameTime gt) 
    {
        Position += Direction * MoveSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    private void cooldown(Entity owner) 
    {
        Position = Vector2.LerpPrecise(Position, owner.Center - HalfSize, 0.15f);
    }
    // main state method.
    private void _stateManager(GameTime gt, Entity owner) 
    {
        switch(ActionStates) 
        {
            case Actions.Ready: ready(owner); break;
            case Actions.Active: active(gt); break;
            case Actions.Cooldown: cooldown(owner); break;
            case Actions.Interrupted: break;
            case Actions.Charging: break;
            case Actions.Completed: break;
            case Actions.Disabled: break;
        }
    }
    // main Update Method
    public void ShootingTime(GameTime gt, Entity owner) 
    {
        _lifeSpan.TickTock(gt);
        if (IsDead || _lifeSpan.TimeHitsFloor()) return;
        WebAnimation.Roll(gt);
        _stateManager(gt, owner);
    }
    public void DrawProjectile(SpriteBatch batch) 
    {
        WebAnimation.Scroll(batch, Bounds);
    }
    // Helper Methods-(ish)
    public void SetTarget(Vector2 location) => Target = location;
    public void SetTarget(Point location) => Target = new Vector2(location.X, location.Y);
}