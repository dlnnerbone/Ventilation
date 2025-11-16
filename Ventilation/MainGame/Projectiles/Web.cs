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
    private readonly Timer readyingTimer;
    
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
    public readonly Timer LifeSpan = new Timer(5f, TimeStates.Down, false, true);
    
    public Vector2 Destination { get; set; } = Vector2.Zero;
    public Vector2 RadiusVector { get; set; } = new Vector2(150, 100);
    
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = MathHelper.Clamp(value, 0, MaxSpeed) * SpeedMulti; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = MathHelper.Clamp(value, MoveSpeed, float.PositiveInfinity) * SpeedMulti; }
    public float SpeedMulti { get => speedMulti; set => speedMulti = Math.Abs(value); }
    
    public float Damage { get => damage; set => damage = MathHelper.Clamp(value, 0, MaxDamage) * DamageMulti; }
    public float MaxDamage { get => maxDamage; set => maxDamage = MathHelper.Clamp(value, Damage, float.PositiveInfinity); }
    public float DamageMulti { get => damageMulti; set => damageMulti = Math.Abs(value); }
    
    public WebClump() : base(0, 0, 16 * 4, 16 * 4, Vector2.UnitX, Actions.Disabled) 
    {
        readyingTimer = new Timer(6f, TimeStates.Down, false, false);
    }
    // general methods
    public float NormalizedSpeedProgress => MoveSpeed / MaxSpeed;
    public float NormalizedDamage => Damage / MaxDamage;
    public float NormalizedLifeSpan => LifeSpan.NormalizedProgress;
    public void SetDestination(Vector2 location) => Destination = location;
    
    public float DistanceFrom(Vector2 location) 
    {
        return Vector2.Distance(Position, location);
    }
    public float DistanceFromSquared(Vector2 location) 
    {
        return Vector2.DistanceSquared(Position, location);
    }
    
    // main methods
    
    public void LoadContent(ContentManager content) 
    {
        var texture = content.Load<Texture2D>("Game/Assets/ProjectileAssets/WebClump/WebClump_Master");
        Atlas = new TileGrid(14, 2, texture);
        Animation = new Animation(texture, Atlas, 0, 5, 3);
        
        Animation.AddPreset("Active", 0, 4);
        Animation.AddPreset("Readying", 14, 27);
        Animation.AddPreset("Waiting", 24, 27);
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
            case Actions.Cooldown: break;
        }
    }
    
    public override void DrawProjectile(SpriteBatch spriteBatch) 
    {
        if (IsDead) return;
        Animation.Animate(spriteBatch, Bounds);
    }
    
    // private methods
    
    private void timeManager(GameTime gt) 
    {
        LifeSpan.TickTock(gt);
        readyingTimer.TickTock(gt);
        
        if (!IsReady) 
        {
            _hasJustEnteredReady = false;
        }
        if (!IsCurrentlyActive) _hasJustEnteredActive = false;
    }
    
    private void _readying() 
    {
        Animation.SetToPreset("Readying");
        
        if (!_hasJustEnteredReady) 
        {
            Animation.Restart();
            readyingTimer.Restart();
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
            _hasJustEnteredActive = true;
        }
        else if (Animation.CurrentFrameIndex >= 24) 
        {
            Animation.SetToPreset("Active");
        }
        Position += Direction * MoveSpeed * buildUpMeter * (float)gt.ElapsedGameTime.TotalSeconds;
    }
}