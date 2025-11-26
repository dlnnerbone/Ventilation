using System;
using GameComponents;
using GameComponents.Interfaces;
using GameComponents.Helpers;
using GameComponents.Rendering;
using GameComponents.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Logic;
namespace Main;
public sealed class WebClump : Projectile, IPoolable<WebClump> 
{
    private readonly Timer readyingTimer;
    private readonly Timer cooldownTimer;
    
    private float moveSpeed = 750f;
    private float maxSpeed = 1000f;
    private float speedMulti = 1f;
    
    private float damage = 25f;
    private float maxDamage = 40f;
    private float damageMulti = 1f;
    
    private float buildUpMeter;
    
    private bool _hasJustEnteredReady = false;
    private bool _hasJustEnteredCooldown = false;
    private bool _hasJustEnteredActive = false;
    //
    public TileGrid Atlas { get; private set; }
    public Animation Animation { get; private set; }
    public readonly Timer LifeSpan = new Timer(5f, TimeStates.Down, false, false);
    
    public Vector2 Destination { get; set; } = Vector2.Zero;
    public Vector2 RadiusVector { get; set; } = new Vector2(150, 100);
    
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = MathHelper.Clamp(value, 0, MaxSpeed) * SpeedMulti; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = MathHelper.Clamp(value, MoveSpeed, float.PositiveInfinity) * SpeedMulti; }
    public float SpeedMulti { get => speedMulti; set => speedMulti = Math.Abs(value); }
    
    public float Damage { get => damage; set => damage = MathHelper.Clamp(value, 0, MaxDamage) * DamageMulti; }
    public float MaxDamage { get => maxDamage; set => maxDamage = MathHelper.Clamp(value, Damage, float.PositiveInfinity); }
    public float DamageMulti { get => damageMulti; set => damageMulti = Math.Abs(value); }
    
    public WebClump() : base(0, 0, 16 * 4, 16 * 4, Vector2.UnitX, Actions.Ready) 
    {
        readyingTimer = new Timer(6f, TimeStates.Down, false, false);
        cooldownTimer = new Timer(10f, TimeStates.Down, false, false);
    }
    // general methods
    public float NormalizedSpeedProgress => MoveSpeed / MaxSpeed;
    public float NormalizedDamage => Damage / MaxDamage;
    public float NormalizedLifeSpan => LifeSpan.NormalizedProgress;
    public void SetDestination(Vector2 location) => Destination = location;
    
    // main methods
    
    public void LoadContent(ContentManager content) 
    {
        var texture = content.Load<Texture2D>("Game/Assets/ProjectileAssets/WebClump/WebClump_Master");
        Atlas = new TileGrid(14, 2, texture);
        Animation = new Animation(texture, Atlas, 0, 5, 3);
        
        Animation.AddPreset("Active", 0, 5);
        Animation.AddPreset("Readying", 14, 28);
        Animation.AddPreset("Waiting", 24, 28);
    }
    
    public override void Reset() 
    {
        Radians = 0;
        Animation.Radians = 0;
        Animation.Restart();
        LifeSpan.Restart();
    }
    
    public override void ShootingTime(GameTime gt) 
    {
        if (IsDead) return;
        timeManager(gt);
        Animation.Advance(gt);
        
        switch(ActionStates) 
        {
            case Actions.Ready: _readying(); break;
            case Actions.Active: active(gt); break;
            case Actions.Cooldown: cooldown(); break;
        }
    }
    
    public override void DrawProjectile(SpriteBatch spriteBatch) 
    {
        if (IsDead) return;
        Animation.Animate(spriteBatch, Bounds);
    }
    
    public void Inflict<T>(T opposer) where T : IHealthComponent 
    {
        opposer.Health -= Damage;
    }
    
    // private methods
    
    private void timeManager(GameTime gt) 
    {
        LifeSpan.TickTock(gt);
        readyingTimer.TickTock(gt);
        cooldownTimer.TickTock(gt);
        
        if (!IsReady) 
        {
            _hasJustEnteredReady = false;
        }
        else if (!IsCurrentlyActive) _hasJustEnteredActive = false;
        if (!InCooldown) _hasJustEnteredCooldown = false;
        
    }
    
    private void _readying() 
    {
        Animation.SetToPreset("Readying");
        
        if (!_hasJustEnteredReady) 
        {
            Animation.Restart();
            readyingTimer.Restart();
            LifeSpan.IsPaused = true;
            LifeSpan.Restart();
            _hasJustEnteredReady = true;
        }
        else if (Animation.CurrentFrameIndex >= 24) 
        {
            Animation.SetToPreset("Waiting");
        }
        
        float progress = Easing.EaseInExpo(readyingTimer.NormalizedProgress);
        buildUpMeter = 1 - progress;
        Position = Destination - HalfSize + Direction * RadiusVector * progress;
    }
    
    private void active(GameTime gt)
    {
        if (!_hasJustEnteredActive) 
        {
            Animation.SetRange(Animation.CurrentFrameIndex, Animation.CurrentFrameIndex + 3);
            LifeSpan.IsPaused = false;
            _hasJustEnteredActive = true;
        }
        else if (Animation.CurrentFrameIndex >= 24) 
        {
            Animation.SetToPreset("Active");
        }
        Position += Direction * MoveSpeed * buildUpMeter * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    
    private void cooldown() 
    {
        if (!_hasJustEnteredCooldown) 
        {
            cooldownTimer.Restart();
            _hasJustEnteredCooldown = true;
        }
        Position = Vector2.LerpPrecise(Position, Destination - HalfSize, 0.1f);
    }
}