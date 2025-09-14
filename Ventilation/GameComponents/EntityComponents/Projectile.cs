using Microsoft.Xna.Framework;
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
    // helper bools
    public bool IsActive => actionState == Actions.Active;
    public bool HasEnded => actionState == Actions.Ended;
    public bool HasCompleted => actionState == Actions.Completed;
    public bool WasInteruppted => actionState == Actions.Interrupted;
    public bool IsReady => actionState == Actions.Ready;
    public bool InRecovery => actionState == Actions.Cooldown;
    public bool IsDisabled => actionState == Actions.Disabled;
    //
    public Actions SetActionState(Actions newState) => actionState = newState;
    public virtual float Angle => (float)Math.Atan2(Direction.Y, Direction.X);
    // methods
    public abstract void ShootingTime(GameTime gt);
    // easy methods
    public void Anchor(Vector2 position) => Position = position;
    public void LookAt(Vector2 location) => Direction = location - Position;
    public void FaceLike(Vector2 direction) => Direction = direction;
    // constructor(s)
    protected Projectile(int x, int y, int width, int height, Vector2 dir) : base(x, y, width, height) 
    {
        ValidateDirection(dir);
        direction = dir;
    }
    protected Projectile(Point location, Point size, Vector2 dir) : base(location, size) 
    {
        ValidateDirection(dir);
        direction = dir;
    }
    protected Projectile(Vector2 location, Vector2 size, Vector2 dir) : base(location, size) 
    {
        ValidateDirection(dir);
        direction = dir;
    }
    protected Projectile(Vector4 VectorModel, Vector2 dir) : base(VectorModel) 
    {
        ValidateDirection(dir);
        direction = dir;
    }
    protected Projectile(Rectangle bounds, Vector2 dir) : base(bounds) 
    {
        ValidateDirection(dir);
        direction = dir;
    }
    private void ValidateDirection(Vector2 vector) 
    {
        if (vector == Vector2.Zero) throw new ArgumentException("Directon Vector cannot have both values as Zero.");
    }
}