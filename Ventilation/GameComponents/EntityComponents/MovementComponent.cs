using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class MovementComponent : IMovementComponent 
{
    protected Vector2 velocity = Vector2.Zero;
    // private fields
    public virtual Vector2 Velocity { get { return velocity; } set { velocity = value; } }
    public virtual Vector2 Direction { get { return Vector2.Normalize(velocity); } }
    public float Velocity_X { get { return velocity.X; } set { velocity.X = value; } }
    public float Velocity_Y { get { return velocity.Y; } set { velocity.Y = value; } }
    public bool IsMovingLeft() => Direction.X < 0;
    public bool IsMovingRight() => Direction.X > 0;
    public bool IsMovingDown() => Direction.Y > 0;
    public bool IsMovingUp() => Direction.Y < 0;
    public bool IsMoving() => IsMovingLeft() || IsMovingRight() || IsMovingDown() || IsMovingUp();
    public MovementComponent() {}
}