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
    private SpriteFont font;
    public PlayerMovement() 
    {
        dashCool = new(0.45f);
        dashCool.AutoRestart = false;

        dashDur = new(0.2f);
        dashDur.AutoRestart = false;

        staminaRegen = new(1.65f);
        staminaRegen.AutoRestart = true;
    }
    public void LoadFont(Game game) 
    {
        font = game.Content.Load<SpriteFont>("PixelatedElegance");
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
        
        if (Input.IsKeyDown(Keys.W)) player.Velocity_Y -= MoveSpeed;
        else if (Input.IsKeyDown(Keys.S)) player.Velocity_Y += MoveSpeed;
        else player.Velocity_Y = MathHelper.Lerp(player.Velocity_Y, 0, LerpSpeed);

        if (Input.IsKeyDown(Keys.A)) player.Velocity_X -= MoveSpeed;
        else if (Input.IsKeyDown(Keys.D)) player.Velocity_X += MoveSpeed;
        else player.Velocity_X = MathHelper.Lerp(player.Velocity_X, 0, LerpSpeed);
    }
    private void Dashing(Player player) 
    {
        player.Velocity = player.Direction * DashForce;
        if (dashDur.TimerIsZero()) 
        {
            IsDashing = false;
            player.IsControllable = true;
            SetMotion(Motions.Idle);
        }
    }
    private void HandleStamina() 
    {
        if (staminaRegen.ElapsedTime <= 0.02f) 
        {
            Stamina += 1;
        }
    }
    private void HandleInputs(Player player) 
    {
        if (player.IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
        
        if (Input.IsKeyDown(Keys.LeftShift) && dashCool.TimerIsZero() && !IsDashing && Stamina <= 0  && player.IsControllable) 
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
        Input.UpdateInputs();
        dashCool.UpdateTimer(gt);
        dashDur.UpdateTimer(gt);
        staminaRegen.UpdateTimer(gt);
        HandleStamina();
        HandleInputs(player);
        switch (Motion) 
        {
            case Motions.Idle: Idle(player); break;
            case Motions.Moving: Moving(player); break;
            case Motions.Dashing: Dashing(player); break;
        }
    }
    public void DrawFontAndTestTimers(SpriteBatch batch) 
    {
        batch.DrawString(font, "DashCooldown is:" + dashCool.ElapsedTime, new Vector2(50, 50), Color.Green);
        batch.DrawString(font, "DashDuration is:" + dashDur.ElapsedTime, new Vector2(50, 100), Color.Purple);
        
    }
}