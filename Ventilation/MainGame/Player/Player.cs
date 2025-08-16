using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Managers;
using GameComponents.Logic;
namespace Main;
public class Player : Entity 
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
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value < 0 ? 0 : value; } }
    public float LerpSpeed { get { return lerpSpeed; } private set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= maxSpeed ? maxSpeed + value * 2 : value; } }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value; } }
    public bool IsDashing { get; set; } = false;
    // player related variables
    public bool IsControllable { get; set; } = true;
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        dashCool = new(0.45f);
        dashCool.AutoRestart = false;

        dashDur = new(0.25f);
        dashDur.AutoRestart = false;

        staminaRegen = new(1.55f);
        staminaRegen.AutoRestart = true;
    }
    public void LoadContent(GraphicsDevice device) 
    {
        PlayerSprite = new(new(device, 1, 1), Color.Red);
        PlayerSprite.SetToData();
    }
    private void Idle() 
    {
        Velocity = Vector2.Lerp(Velocity, Vector2.Zero, LerpSpeed);
        IsDashing = false;
        Velocity_Y = Velocity_Y >= -0.5f && Velocity_Y <= 0.5f ? 0 : Velocity_Y;
        Velocity_X = Velocity_X >= -0.5f && Velocity_X <= 0.5f ? 0 : Velocity_X;
    }
    private void Moving() 
    {
        IsDashing = false;
        if (Input.IsKeyDown(Keys.W)) Velocity_Y += -moveSpeed;
        else if (Input.IsKeyDown(Keys.S)) Velocity_Y += moveSpeed;
        if (Input.IsKeyDown(Keys.A)) Velocity_X += -moveSpeed;
        else if (Input.IsKeyDown(Keys.D)) Velocity_X += moveSpeed;
    }
    private void HandleMotionInput() 
    {
        if (IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
    }
    private void HandleMotionStates() 
    {
        HandleMotionInput();
        Velocity = Vector2.Clamp(Velocity, new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));
        switch (Motion) 
        {
            case Motions.Idle: Idle(); break;
            case Motions.Moving: Moving(); break;
            case Motions.Dashing: break;
        }
    }
    public void UpdateLogic(GameTime gt) 
    {
        Input.UpdateInputs();
        MoveAndSlide(gt);
        HandleMotionStates();
    }
    public void Draw(SpriteBatch batch) 
    {
        PlayerSprite.Draw(batch, Bounds);
    }
    
}