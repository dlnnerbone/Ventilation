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
    public Timer DecayTime { get; set; } = new(5f);
    public float BulletSpeed { get { return bulletSpd; } set { bulletSpd = value * speedMulti; } }
    public float Damage { get { return dmg; } set { dmg = value * dmgMulti; } }
    public float SpeedMultiplier { get { return speedMulti; } set { speedMulti = Math.Abs(value); } }
    public float DamageMultiplier { get { return dmgMulti; } set { dmgMulti = Math.Abs(value); } }
    // methods
    public override void Reset() 
    {
        DecayTime.Restart();
        SetActionMode(Actions.Ready);
    }
    private void UpdatePosition(GameTime gt) 
    {
        DecayTime.TickTock(gt);
        if (DecayTime.TimerIsZero) return;
        Position += Direction * BulletSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    public override void Update(GameTime gt) 
    {
        switch (ActionState) 
        {
            case Actions.Ready: break;
            case Actions.Fly: UpdatePosition(gt); break;
            case Actions.End: break;
        }
    }
    // constructors
    public Bullet(int x, int y, int width, int height, Vector2 direction) : base(x, y, width, height, direction) {}
    public Bullet(Point point, Point size, Vector2 direction) : base(point, size, direction) {}
    public Bullet(Vector2 location, Vector2 size, Vector2 direction) : base(location, size, direction) {}
    public Bullet(Rectangle bounds, Vector2 direction) : base(bounds, direction) {}
    public Bullet(Vector4 data, Vector2 direction) : base(data, direction) {}
}