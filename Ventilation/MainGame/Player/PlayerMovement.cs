using Microsoft.Xna.Framework;
using GameComponents;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Input;
using GameComponents.Logic;
namespace Main;
public sealed class PlayerMotion 
{
    private Motions Motion = Motions.Idle;
    public InputManager Input = new();
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float lerpSpeed = 0.15f;
    private float dashForce = 2000f;
    private Timer dashCool, dashDur, staminaRegen;
    private int stamina = 3;
    private int maxStamina = 3;
    // private fields for movement utilities
    public Motions SetMotion(Motions motion) => Motion = motion;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = MathHelper.Clamp(value, 0, MaxSpeed); } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value < 0 ? 0 : value; } }
    public float LerpSpeed { get { return lerpSpeed; } private set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= maxSpeed ? maxSpeed + value * 2 : value; } }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value; } }
    public bool IsDashing { get; set; } = false;
    // Player Movement methods
    private void Idle(Player player) 
    {
        MoveSpeed = MathHelper.Lerp(MoveSpeed, 0, LerpSpeed);
        MoveSpeed = MoveSpeed >= -1 && MoveSpeed <= 1 ? 0 : MoveSpeed;
        IsDashing = false;
    }
    private void Moving(Player player) 
    {
        IsDashing = false;
        MoveSpeed += 50;
        player.Velocity.Normalize();
        if (Input.IsKeyDown(Keys.W)) player.Velocity_Y *= -MoveSpeed;
        else if (Input.IsKeyDown(Keys.S)) player.Velocity_Y *= MoveSpeed;
        else player.Velocity_Y = MathHelper.Lerp(player.Velocity_Y, 0, LerpSpeed);

        if (Input.IsKeyDown(Keys.A)) player.Velocity_X *= -MoveSpeed;
        else if (Input.IsKeyDown(Keys.D)) player.Velocity_X *= MoveSpeed;
        else player.Velocity_X = MathHelper.Lerp(player.Velocity_X, 0, LerpSpeed);
    }
    private void Dashing(Player player) {}
    private void HandleInput(Player player) {}
    public void HandlePlayerMotion(Player player) 
    {
        Input.UpdateInputs();
        HandleInput(player);
        switch (Motion) 
        {
            case Motions.Idle: Idle(player); break;
            case Motions.Moving: Moving(player); break;
            case Motions.Dashing: Dashing(player); break;
        }
    }
}