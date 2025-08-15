using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Managers;
using GameComponents.Entity;
namespace Main;
public sealed class PlayerMotion 
{
    private Motions Motion = Motions.Idle;
    public InputManager Input = new();
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float lerpSpeed = 0.15f;
    private float dashForce = 2000f;
    // private fields
    public Motions SetMotion(Motions motion) => Motion = motion;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value < 0 ? 0 : value; } }
    public float LerpSpeed { get { return lerpSpeed; } set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= maxSpeed ? value * 2 : value; } }
    public bool IsDashing { get; set; } = false;
    
    private void Idle(Vector2 velocity) 
    {
        velocity = Vector2.Lerp(velocity, Vector2.Zero, lerpSpeed);
        IsDashing = false;
        velocity.X = velocity.X >= -1 && velocity.X <= 1 ? 0 : velocity.X;
        velocity.Y = velocity.Y >= -1 && velocity.Y <= 1 ? 0 : velocity.Y;
    }
    private void Moving(Vector2 velocity) 
    {
        IsDashing = false;
        if (Input.IsKeyDown(Keys.W)) velocity.Y -= moveSpeed;
        else if (Input.IsKeyDown(Keys.S)) velocity.Y += moveSpeed;
        if (Input.IsKeyDown(Keys.A)) velocity.X -= moveSpeed;
        else if (Input.IsKeyDown(Keys.D)) velocity.X += moveSpeed;
    }
    private void Dashing(Entity entity) {}
    private void HandleMotionInput(Player player) 
    {
        if (player.IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
    }
    public void HandleMotionStates(Player player) 
    {
        Input.UpdateInputs();
        HandleMotionInput(player);
        player.Velocity = Vector2.Clamp(player.Velocity, new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));
        switch (Motion) 
        {
            case Motions.Idle: Idle(player.Velocity); break;
            case Motions.Moving: Moving(player.Velocity); break;
            case Motions.Sliding: Dashing(player); break;
        }
    }
    
}