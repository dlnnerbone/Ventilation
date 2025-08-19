using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents.Logic;
using GameComponents.Managers;
using GameComponents;
namespace Main;
public sealed class PlayerMovement 
{
    private InputManager Input = new();
    private Motions Motion = Motions.Idle;
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float lerpSpeed = 0.15f;
    private float dashForce = 2000f;
    private Timer dashCool, dashDur, staminaRegen;
    private int stamina = 3;
    private int maxStamina = 3;
    // private fields
    public Motions SetMotion(Motions motion) => Motion = motion;
    public Motions MotionState => Motion;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float LerpSpeed { get { return lerpSpeed; } private set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= MaxSpeed ? MaxSpeed * 2 : value; } }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value < 0 ? stamina + 1 : value; } }
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
    private void Idle(Player player) {}
    private void Moving(Player player) {}
    private void Dashing(Player player) {}
    private void HandleStamina() 
    {
        if (staminaRegen.TimerIsZero()) 
        {
            Stamina += 1;
        }
    }
    private void UpdateTimers(GameTime gt) 
    {
        dashCool.UpdateTimer(gt);
        dashDur.UpdateTimer(gt);
        staminaRegen.UpdateTimer(gt);
    }
    private void HandleInputs(Player player) 
    {
        Input.UpdateInputs();
        if (player.IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
        if (Input.IsKeyDown(Keys.LeftShift) && dashCool.TimerIsZero() && !IsDashing && player.IsControllable && Stamina <= 0) 
        {
            IsDashing = true;
            player.IsControllable = false;
            Stamina -= 1;
            dashCool.RestartTimer();
            dashDur.RestartTimer();
            SetMotion(Motions.Dashing);
        }
    }
    public void HandlePlayerMovement(GameTime gt, Player player) 
    {
        HandleStamina();
        UpdateTimers(gt);
        HandleInputs(player);
        switch (Motion) 
        {
            case Motions.Idle: Idle(player); break;
            case Motions.Moving: Moving(player); break;
            case Motions.Dashing: Dashing(player); break;
        }
    }
}