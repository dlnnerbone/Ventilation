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
    private float speed = 750f;
    private float maxSpeed = 750f;
    private float speedMulti = 1f;
    private float damage = 25f;
    private float maxDamage = 50f;
    private float damageMulti = 1;

    private bool _justBecameInCooldown = false;
    private bool _justTurnedToReady = false;
    private float _chargingMeter;

    private readonly Timer readyingTimer = new(3f, TimeStates.Down, false, false);
    private readonly Timer cooldownMeter = new(8f, TimeStates.Down, false, false);
    // public properties
    public readonly Timer LifeSpan = new Timer(5f, TimeStates.Down, false, false);
    public float MoveSpeed { get => speed; set => speed = MathHelper.Clamp(value, 0, MaxSpeed) * speedMulti; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = MathHelper.Clamp(value, speed, float.PositiveInfinity); }
    public float Damage { get => damage; set => damage = MathHelper.Clamp(value, 0, maxDamage) * damageMulti; }
    public float MaxDamage { get => maxDamage; set => maxDamage = MathHelper.Clamp(value, damage, float.PositiveInfinity) * damageMulti; }
    public float SpeedMulti { get => speedMulti; set => speedMulti = Math.Abs(value); }
    public float DamageMulti { get => damageMulti; set => damageMulti = Math.Abs(value); }
    public Vector2 TargetLocation { get; set; } = Vector2.Zero;
    public Vector2 RadiusVector { get; set; } = new Vector2(65, 45);
    
    public TextureAtlas TextureAtlas { get; private set; }
    public Animation Animation { get; private set; }
    // helper methods
    public float NormalizedSpeed => MoveSpeed / MaxSpeed;
    public float NormalizedDamage => Damage / MaxDamage;
    public float NormalizedLifeSpan => LifeSpan.NormalizedProgress;

    public void SetTarget(Vector2 target) => TargetLocation = target;
    
    public float DistanceFrom(Vector2 location, bool centered = true) 
    {
        return Vector2.Distance(location, centered ? Center : Position);
    }
    public float DistanceFromSquared(Vector2 location, bool centered = true) 
    {
        return Vector2.DistanceSquared(location, centered ? Center : Position);
    }
    
    public WebClump(int width, int height, Vector2 dir, Actions flags) : base(0, 0, width, height, dir, flags) {}
    
    
    private void readying()
    {
        if (!readyingTimer.TimeHitsFloor() && !_justTurnedToReady) 
        {
            readyingTimer.Restart();
            readyingTimer.IsPaused = false;
            _justTurnedToReady = true;
        }
        float progress = Easing.EaseOutExpo(readyingTimer.NormalizedProgress);
        Vector2 offset = new Vector2(HalfSize.X, HalfSize.Y + 25f);
        _chargingMeter = 1 - progress;
        Position = TargetLocation - offset + Direction * RadiusVector * MathHelper.Clamp(progress, 0.25f, 1f);
    }
    
    private void _cooldown() 
    {
        if (!cooldownMeter.TimeHitsFloor() && !_justBecameInCooldown) 
        {
            cooldownMeter.Restart();
            cooldownMeter.IsPaused = false;
            _justBecameInCooldown = true;
        }
        float progress = 1 - cooldownMeter.NormalizedProgress;
        Position = Vector2.LerpPrecise(Position, TargetLocation - HalfSize, progress);
    }
    private void _active(GameTime gt) 
    {
        Position += Direction * MoveSpeed * _chargingMeter * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    
    public override void Reset() 
    {
        OverrideFlags(Actions.Disabled);
        Radians = 0;
        LifeSpan.Restart();
        Animation.Stop();
        Animation.IsVisible = false;
    }
    
    public void LoadContent(ContentManager content) 
    {
        TextureAtlas = new TextureAtlas(3, 2, 48, 32);
        Animation = new Animation(content.Load<Texture2D>("GameAssets/ProjectileTextures/WebClump_Active"), TextureAtlas, 0, 5, 5);
    }
    public override void ShootingTime(GameTime gt) 
    {
        if (IsDead) return;
        timerManagement(gt);
        Animation.Advance(gt);

        Animation.LayerDepth = MathHelper.Clamp((float)Math.Sin(Radians), 0.4f, 0.65f);
        
        switch(ActionStates) 
        {
            case Actions.Ready: readying(); break;
            case Actions.Active: _active(gt); break;
            case Actions.Cooldown: _cooldown(); break; 
        }
    }
    private void timerManagement(GameTime gt) 
    {
        LifeSpan.TickTock(gt);
        readyingTimer.TickTock(gt);
        cooldownMeter.TickTock(gt);
        
        if (!IsReady) 
        {
            readyingTimer.IsPaused = true;
            readyingTimer.Restart();
            _justTurnedToReady = false;
        }
        if (!InCooldown) 
        {
            _justBecameInCooldown = false;
            cooldownMeter.IsPaused = true;
            cooldownMeter.Restart();
        }
    }
    public override void DrawProjectile(SpriteBatch batch) 
    {
        if (IsDead) return;
        Animation.Animate(batch, Bounds);
    }
}