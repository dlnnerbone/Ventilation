using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Managers;
using GameComponents.Entity;
namespace Main;
public sealed class PlayerMotion 
{
    private Motions Motion = Motions.Idle;
    public InputManager Input;
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float speedMulti = 1;
    private float dashForce = 2000f;
    // private fields
    public bool IsDashing { get; set; } = false;
    private void Idle(Vector2 velocity) {}
    private void Moving(Vector2 velocity) {}
    private void Dashing(Entity entity) {}
    private void HandleMotionInput() {}
    public void HandleMotionStates(Entity entity) 
    {
        Input.UpdateInputs();
        HandleMotionInput();
        switch (Motion) 
        {
            case Motions.Idle: Idle(entity.Velocity); break;
            case Motions.Moving: Moving(entity.Velocity); break;
            case Motions.Sliding: Dashing(entity); break;
        }
    }
    
}