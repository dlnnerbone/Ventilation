using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Helpers;
using GameComponents.Rendering;
using GameComponents.Logic;
using GameComponents.Entity;
using System;
using GameComponents;
using GameComponents.helpers;
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
    public Timer BulletDuration { get; set; } = new(5);
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value * speedMulti; } }
    public float SpeedMultiplier { get { return speedMulti; } set { speedMulti = Math.Abs(value); } }
    public float Damage { get { return dmg; } set { dmg = Math.Abs(value); } }
    public float DamageMultiplier { get { return dmgMulti; } set { dmgMulti = Math.Abs(value); } }
    // main methods.
    public void LoadContent(GraphicsDevice device) 
    {
        BulletTexture = new(new Texture2D(device, 1, 1), Color.White);
        BulletTexture.SetToData();
    }
    /* public void LoadContent(ContentManager content) 
    {
        BulletTexture = new()
    } */
    public override void ShootingTime(GameTime gt) 
    {
        if (IsDisabled) return;
        BulletDuration.TickTock(gt);
        switch (ActionState) 
        {
            case Actions.Ready: break;
            case Actions.Charging: break;
            case Actions.Active: Active(gt); break;
            case Actions.Interrupted: Interrupted(gt); break;
            case Actions.Cooldown: break;
            case Actions.Ended: break;
            case Actions.Completed: break;
            case Actions.Disabled: break;
        }
    }
    // private methods for ShootingTime
    private void Active(GameTime gt) 
    {
        Position += Direction * MoveSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    private void Interrupted(GameTime gt) 
    {
        if (BulletDuration.TimerIsZero) BulletDuration.Restart();
        Position += InterpolationHelper.LinearShake(5, 5, BulletDuration.TimeSpan);
    }
    // drawing
    public void Draw(SpriteBatch batch) 
    {
        if (IsDisabled) return;
        BulletTexture.Draw(batch, Bounds, Angle);
    }
    // constructors
    public Bullet(Vector2 dir, int x, int y, int width = 32, int height = 32) : base(x, y, width, height, dir) {}
}