using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents.Logic;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
namespace Main;
public sealed class PlayerMovement 
{
    private InputManager Input = new();
    private Motions Motion = Motions.Idle;
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float lerpSpeed = 0.15f;
    private float speedMulti = 1f;
    private float dashForce = 2000f;
    private Timer dashCool, dashDur, staminaRegen;
    private int stamina = 3;
    private int maxStamina = 3;
    // private fields
    public Motions SetMotion(Motions motion) => Motion = motion;
    public Motions MotionState => Motion;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float LerpSpeed { get { return lerpSpeed; } internal set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= MaxSpeed ? MaxSpeed * 2 : value; } }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value < 0 ? stamina + 1 : value; } }
    public float SpeedMultiplier { get { return speedMulti; } set { speedMulti = value < 0 ? 0 : value; } }
    public bool IsDashing { get; set; } = false;
    public PlayerMovement() 
    {
        dashCool = new(0.45f);
        dashCool.AutoRestart = false;

        dashDur = new(0.2f);
        dashDur.AutoRestart = false;

        staminaRegen = new(1.65f);
        staminaRegen.AutoRestart = true;
    }
    private void Idle(Player player) 
    {
        player.Velocity = Vector2.Lerp(player.Velocity, Vector2.Zero, LerpSpeed);
        IsDashing = false;
        player.Velocity_X = player.Velocity_X >= -1 && player.Velocity_X <= 1 ? 0 : player.Velocity_X;
        player.Velocity_Y = player.Velocity_Y >= -1 && player.Velocity_Y <= 1 ? 0 : player.Velocity_Y;
    }
    private void Moving(Player player) 
    {
        IsDashing = false;
        player.Velocity = Vector2.Clamp(player.Velocity, new Vector2(-MaxSpeed, -MaxSpeed), new Vector2(MaxSpeed, MaxSpeed));
        
        if (Input.IsKeyDown(Keys.W)) player.Velocity_Y -= MoveSpeed * speedMulti;
        else if (Input.IsKeyDown(Keys.S)) player.Velocity_Y += MoveSpeed * speedMulti;
        else player.Velocity_Y = MathHelper.Lerp(player.Velocity_Y, 0, LerpSpeed);

        if (Input.IsKeyDown(Keys.A)) player.Velocity_X -= MoveSpeed * speedMulti;
        else if (Input.IsKeyDown(Keys.D)) player.Velocity_X += MoveSpeed * speedMulti;
        else player.Velocity_X = MathHelper.Lerp(player.Velocity_X, 0, LerpSpeed);
    }
    private void Dashing(Player player) 
    {
        player.Velocity = player.Direction * DashForce * speedMulti;
        if (dashDur.TimerIsZero) 
        {
            IsDashing = false;
            player.IsControllable = true;
            SetMotion(Motions.Idle);
        }
    }
    private void HandleStamina() 
    {
        if (staminaRegen.TimeSpan <= 0.02f) 
        {
            Stamina += 1;
        }
    }
    private void HandleInputs(Player player) 
    {
        bool canDash = dashCool.TimerIsZero && !IsDashing && Stamina > 0 && player.Velocity != Vector2.Zero;
        if (player.IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
        
        if (Input.IsKeyPressed(Keys.LeftShift) && canDash && player.IsControllable) 
        {
            IsDashing = true;
            player.IsControllable = false;
            Stamina -= 1;
            dashCool.Restart();
            dashDur.Restart();
            
            SetMotion(Motions.Dashing);
        }
    }
    private void HandleTimers(GameTime gt) 
    {
        dashCool.UpdateTimer(gt);
        dashDur.UpdateTimer(gt);
        staminaRegen.UpdateTimer(gt);
    }
    public void HandlePlayerMovement(GameTime gt, Player player) 
    {
        Input.UpdateInputs();
        HandleTimers(gt);
        HandleStamina();
        HandleInputs(player);
        switch (Motion) 
        {
            case Motions.Idle: Idle(player); break;
            case Motions.Moving: Moving(player); break;
            case Motions.Dashing: Dashing(player); break;
        }
    }
}