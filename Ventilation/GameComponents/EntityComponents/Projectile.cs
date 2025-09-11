using Microsoft.Xna.Framework;
using GameComponents;
using System;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public abstract class Projectile : BodyComponent, IDirection
{
    protected Vector2 direction;
    private Actions actionState = Actions.Ready;
    // privaate fields
    public Actions ActionState { get { return actionState; } protected set { actionState = value; } }
    public virtual Vector2 Direction 
    {
        get => direction;
        set => direction = Vector2.Normalize(value);
    }
    public bool IsActive { get; set; } = true;
    public Actions SetActionState(Actions newState) => actionState = newState;
    public virtual float Angle => (float)Math.Atan2(Direction.Y, Direction.X);
    // methods
    public abstract void Shoot(GameTime gt);
    // constructor(s)
    protected Projectile(int x, int y, int width, int height, Vector2 dir) : base(x, y, width, height) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("dumb fuck, dont make direction zero stupid idiot");
        direction = dir;
    }
    protected Projectile(Point location, Point size, Vector2 dir) : base(location, size) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("Get a load of this guy! no like seriously! he tried.. *wheeze* HE TRIED TO MAKE DIRECTION A ZERO-");
        direction = dir;
    }
    protected Projectile(Vector2 location, Vector2 size, Vector2 dir) : base(location, size) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("how tf did you pass middle school bro. how many times does the compiler have to tell you that you can'T MAKE UNIT FUCKING VECTORS ZERO");
        direction = dir;
    }
    protected Projectile(Vector4 VectorModel, Vector2 dir) : base(VectorModel) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("What if I told you Vectors with both values as a zero are really gay?");
        direction = dir;
    }
    protected Projectile(Rectangle bounds, Vector2 dir) : base(bounds) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException("when the Vector gives you wieners!!!");
        direction = dir;
    }
}