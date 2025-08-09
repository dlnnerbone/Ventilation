using Microsoft.Xna.Framework;
namespace GameComponents.Entity;
public abstract class Entity : BodyComponent 
{
    protected MovementComponent movement;
    protected HealthComponent healthCom;
    // protected fields.
    public virtual Vector2 Velocity { get { return movement.Velocity; } set { movement.Velocity = value; } }
    public virtual Vector2 Direction { get { return movement.Direction; } }
    public float Velocity_X { get { return movement.Velocity_X; } set { movement.Velocity_X = value; } }
    public float Velocity_Y { get { return movement.Velocity_Y; } set { movement.Velocity_Y = value; } }
    public bool IsMovingLeft() => movement.IsMovingLeft();
    public bool IsMovingRight() => movement.IsMovingRight();
    public bool IsMovingDown() => movement.IsMovingDown();
    public bool IsMovingUp() => movement.IsMovingUp();
    public bool IsMoving() => movement.IsMoving();
    // velocity based properties.
    public float Health { get { return healthCom.Health; } set { healthCom.Health = value; } }
    public float MinHealth { get { return healthCom.MinHealth; } set { healthCom.MinHealth = value; } }
    public float MaxHealth { get { return healthCom.MaxHealth; } set { healthCom.MaxHealth = value; } }
    // methods.
    public void Destroy() => healthCom.Destroy();
    /// <summary>
    /// the default method for derived classes to use to make the Entity have smoother, more controlled motion.
    /// override the method if you want a different way of handling an object's motion, this is also considered an optional method too.
    /// </summary>
    /// <param name="gt">the parameter for the Method to run by.</param>
    /// <returns></returns>
    protected virtual Vector2 MoveAndSlide(GameTime gt) 
    {
        return Position += Velocity * (float)gt.ElapsedGameTime.TotalSeconds;
    }
    public bool Intersects(Entity other) => this.Intersects(other.Bounds);
    // constructors
    public Entity(int x, int y, int width, int height, float Health, float minHealth = 0, float maxHealth = 100) : base(x, y, width, height)
    {
        healthCom = new(Health, minHealth, maxHealth);
        movement = new();
    }
    public Entity(Point location, Point Size, float Health, float min = 0, float max = 100) : base(location, Size) 
    {
        healthCom = new(Health, min, max);
        movement = new();
    }
    public Entity(Vector2 position, Vector2 size, float HP, float min = 0, float max = 100) : base(position, size) 
    {
        healthCom = new(HP, min, max);
        movement = new();
    }
    public Entity(Rectangle otherBounds, float hp, float min = 0, float max = 100) : base(otherBounds) 
    {
        healthCom = new(hp, min, max);
        movement = new();
    }
    public Entity(Vector4 data, float hp, float min = 0, float max = 100) : base(data) 
    {
        healthCom = new(hp, min, max);
        movement = new();
    }
}