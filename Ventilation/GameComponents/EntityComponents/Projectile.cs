using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Entity;
public class Projectile : BodyComponent
{
    protected float scalarSpeed = 0;
    protected Vector2 direction = Vector2.One;
    private Actions actionState = Actions.Ready;
    // private fields
    public Vector2 Origin { get { return Position; } set { Position = value; } }
    public virtual float ScalarSpeed { get { return scalarSpeed; } set { scalarSpeed = Math.Abs(value); } }
    public virtual Vector2 Direction { get { return direction; } set { direction = Vector2.Normalize(value); } }
    public Actions ActionMode(Actions newState) => actionState = newState;
    //  Constructors
    public Projectile(int x, int y, int width, int height, Vector2 dir, float speed = 100) : base(x, y, width, height) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("Direction can not have both unit values as zero.");
        Direction = dir;
        scalarSpeed = speed;
    }
    public Projectile(Point location, Point size, Vector2 dir, float speed = 100) : base(location, size) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("Direction can not have both unit values as zero.");
        Direction = dir;
        scalarSpeed = speed;
    }
    public Projectile(Vector2 position, Vector2 size, Vector2 dir, float speed = 100) : base(position, size) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("Direction can not have both unit values as zero.");
        Direction = dir;
        scalarSpeed = speed;
    }
    public Projectile(Vector4 VectorModel, Vector2 dir, float speed = 100) : base(VectorModel) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("Direction can not have both unit values as zero.");
        Direction = dir;
        scalarSpeed = speed;
    }
    // methods
    public void LookAt(Vector2 location) => Direction = location - Origin;
    public void SetPointSimilarity(Vector2 target) => Direction = target;
    public virtual void SetLaunch(GameTime gt) {}
}