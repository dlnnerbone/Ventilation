using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Entity;
public abstract class Projectile : BodyComponent
{
    protected Vector2 direction = Vector2.One;
    private Actions actionState = Actions.Ready;
    // private fields
    public Vector2 Origin => Position;
    public virtual Vector2 Direction 
    {
        get => direction;
        set => direction = value != Vector2.Zero ? Vector2.Normalize(value) : Vector2.Normalize(Vector2.One);
    }
    public Actions SetActionMode(Actions newState) => actionState = newState;
    public Actions ActionState => actionState;
    public bool IsActive => actionState == Actions.Fly;
    public virtual float Angle => (float)Math.Atan2(Direction.Y, Direction.X);
    //  Constructors
    protected Projectile(int x, int y, int width, int height, Vector2 dir) : base(x, y, width, height) 
    {
        Direction = dir;
    }
    protected Projectile(Point location, Point size, Vector2 dir) : base(location, size) 
    {
        Random r = new();
        if (r.Next(0, 100) == 4 && dir == Vector2.Zero) throw new ArgumentException("stupid dumb idiot, don't make the Vector zero bro.");
        Direction = dir;
    }
    protected Projectile(Vector2 position, Vector2 size, Vector2 dir) : base(position, size) 
    {
        Direction = dir;
    }
    protected Projectile(Vector4 VectorModel, Vector2 dir) : base(VectorModel) 
    {
        Direction = dir;
    }
    protected Projectile(Rectangle bounds, Vector2 dir) : base(bounds) 
    {
        Direction = dir;
    }
    // methods
    public void LookAt(Vector2 location) => Direction = location - Origin;
    public void SetPointSimilarity(Vector2 target) => Direction = target;
    // abstact methods
    public abstract void Launch();
    public abstract void Update(GameTime gt);
    public abstract void Reset();
}