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
    private float speed = 100f;
    private float maxSpeed = 100f;
    private float speedMulti = 1f;
    private float damage = 25f;
    private float maxDamage = 50f;
    private float damageMulti = 1;
    private float radius = 50f;
    private float maxRadius = 100f;

    private bool _justTurnedToReady = false;

    private Timer readyingTimer = new(3f, TimeStates.Down, false, true);
    // public properties
    public Timer LifeSpan { get; private set; } = new Timer(5f, TimeStates.Down, false, true);
    public float MoveSpeed { get => speed; set => speed = MathHelper.Clamp(value, 0, MaxSpeed) * speedMulti; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = MathHelper.Clamp(value, speed, float.PositiveInfinity); }
    public float Damage { get => damage; set => damage = MathHelper.Clamp(value, 0, maxDamage) * damageMulti; }
    public float MaxDamage { get => maxDamage; set => maxDamage = MathHelper.Clamp(value, damage, float.PositiveInfinity) * damageMulti; }
    public float Radius { get => radius; set => radius = MathHelper.Clamp(value, 0, maxRadius); }
    public float MaxRadius { get => maxRadius; set => maxRadius = MathHelper.Clamp(value, radius, float.PositiveInfinity); }
    public float SpeedMulti { get => speedMulti; set => speedMulti = Math.Abs(value); }
    public float DamageMulti { get => damageMulti; set => damageMulti = Math.Abs(value); }
    public Vector2 TargetLocation { get; set; } = Vector2.Zero;
    
    public TextureAtlas TextureAtlas { get; private set; }
    public Animation Animation { get; private set; }
    // helper methods
    public float NormalizedSpeed => MoveSpeed / MaxSpeed;
    public float NormalizedDamage => Damage / MaxDamage;
    public float NormalizedRadius => Radius / MaxRadius;
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
        Position = TargetLocation - HalfSize + Direction * MaxRadius * Easing.EaseInSine(readyingTimer.NormalizedProgress);
    }
    
    public override void Reset() 
    {
        base.Reset();
        LifeSpan.Restart();
        Animation.Stop();
        Animation.IsVisible = false;
    }
    
    public void LoadContent(ContentManager content) 
    {
        TextureAtlas = new TextureAtlas(3, 2, 48, 32);
        Animation = new Animation(content.Load<Texture2D>("GameAssets/ProjectileTextures/WebClump_Active"), TextureAtlas, 0, 5);
        Animation.FPS = 5;
    }
    public override void ShootingTime(GameTime gt) 
    {
        if (IsDead) return;
        Animation.Roll(gt);
        readyingTimer.TickTock(gt);
        
        if (!IsReady) 
        {
            readyingTimer.IsPaused = true;
            readyingTimer.Restart();
            _justTurnedToReady = false;
        }
        
        switch(ActionStates) 
        {
            case Actions.Ready: readying(); break;
            case Actions.Active: break;
            case Actions.Cooldown: break;
        }
    }
    public override void DrawProjectile(SpriteBatch batch) 
    {
        if (IsDead) return;
        Animation.Scroll(batch, Bounds);
    }
}