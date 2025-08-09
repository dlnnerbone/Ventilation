using Microsoft.Xna.Framework;
namespace GameComponents.Interfaces;
public interface IMovementComponent 
{
    public Vector2 Velocity { get; set; }
}