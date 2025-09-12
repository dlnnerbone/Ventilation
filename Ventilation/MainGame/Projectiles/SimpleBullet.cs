using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Rendering;
using GameComponents.Logic;
using GameComponents.Entity;
using System;
using GameComponents;
namespace Main;
public sealed class Bullet : Projectile 
{
    private float moveSpd = 200f;
    private float spdMulti = 1;
    private float dmg = 20;
    private float dmgMulti = 1;
    // private fields
    public Sprite BulletTexture { get; private set; }
    public float MoveSpeed { get { return moveSpd; } set { moveSpd = value * spdMulti; } }
    public float SpeedMultiplier { get { return spdMulti; } set { spdMulti = Math.Abs(value); } }
    public float Damage { get { return dmg; } private set { dmg = value * dmgMulti; } }
    public float DamageMultiplier { get { return dmgMulti; } set { dmg = Math.Abs(value); } }
    // constructors
    public Bullet(int x, int y, int width, int height, Vector2 dir) : base(x, y, width, height, dir) {}
    public Bullet(Point location, Point size, Vector2 dir) : base(location, size, dir) {}
    public Bullet(Vector2 location, Vector2 size, Vector2 dir) : base(location, size, dir) {}
    public Bullet(Vector4 model, Vector2 dir) : base(model, dir) {}
    public Bullet(Rectangle otherBounds, Vector2 dir) : base(otherBounds, dir) {}
    public void LoadContent(GraphicsDevice device) 
    {
        BulletTexture = new(new Texture2D(device, 1, 1), Color.White);
        BulletTexture.SetToData();
    }
    private void PositionUpdating(GameTime gt) 
    {
        Position += Direction * MoveSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    public override void ShootingTime(GameTime gt) 
    {
        switch (ActionState) 
        {
            case Actions.Ready: break;
            case Actions.Fly: break;
            case Actions.End: break;
        }
    }
    
}