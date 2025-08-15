using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Logic;
namespace Main;
public class Player : Entity 
{
    private InputManager Input = new();
    private float moveSpeed = 50f;
    private float dashForce = 2000f;
    private float maxSpeed = 750f;
    private int stamina = 3;
    private int maxStamina = 3;
    private float speedMultiplier = 1;
    private bool canDash = true;
    private Actions ActionState = Actions.Ready;
    private Motions MotionState = Motions.Idle;
    private Timer dashCool, dashDur, staminaRegen, attackCool;
    // private fields
    public Sprite PlayerSprite { get; set; }
    public Actions SwitchAction(Actions action) => ActionState = action;
    public Motions SetMotion(Motions motionType) => MotionState = motionType;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public int MaxStamina { get { return maxStamina; } set { maxStamina = value < 0 ? 0 : value; } }
    public float SpeedMultiplier { get { return speedMultiplier; } set { speedMultiplier = value <= 0 ? 0.15f : value; } }
    public float DashForce { get { return dashForce; } set { dashForce = value; } }
    public bool IsControllable { get; set; } = true;
    public bool IsAlive { get; set; } = true;
    public bool IsDashing { get; set; } = false;
    public bool CanDash { get { return canDash; } private set { canDash = value; } }
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        dashCool = new(0.4f);

        dashDur = new(0.2f);

        staminaRegen = new(1.5f);
        staminaRegen.AutoRestartOnZero();

        attackCool = new(0.65f);
        
    }
    public void LoadContent(GraphicsDevice device) 
    {
        PlayerSprite = new(new Texture2D(device, 1, 1), Color.Red);
        PlayerSprite.SetToData();
    }
    private void Idle() 
    {
        Velocity = Vector2.Lerp(Velocity, Vector2.Zero, 0.1f);
        Velocity_Y = Velocity_Y >= -1 && Velocity_Y <= 1 ? 0 : Velocity_Y;
        Velocity_X = Velocity_X >= -1 && Velocity_X <= 1 ? 0 : Velocity_X;
        IsDashing = false;
    }
    private void Moving() 
    {
        IsDashing = false;
        if (Input.IsKeyDown(Keys.W)) Velocity_Y += -MoveSpeed;
        else if (Input.IsKeyDown(Keys.S)) Velocity_Y += MoveSpeed;
        if (Input.IsKeyDown(Keys.A)) Velocity_X += -MoveSpeed;
        else if (Input.IsKeyDown(Keys.D)) Velocity_X += MoveSpeed;
    }
    private void HandleMotionInput() 
    {
        if (IsControllable && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.D)))
        {
            SetMotion(Motions.Moving);
        }
        else if (!IsDashing) SetMotion(Motions.Idle);
    }
    private void HandleActionInput() {}
    private void HandleMotionStates() 
    {
        HandleMotionInput();
        switch (MotionState) 
        {
            case Motions.Idle: Idle(); break;
            case Motions.Moving: Moving(); break;
            case Motions.Sliding: break;
        }
    }
    private void HandleActionStates() 
    {
        HandleActionInput();
        switch (ActionState) 
        {
            case Actions.Ready: break;
            case Actions.Fly: break;
            case Actions.End: break;
        }
    }
    private void HandleStates() 
    {
        HandleActionStates();
        HandleMotionStates();
    }
    public void UpdateLogic(GameTime gt) 
    {
        Input.UpdateInputs();
        Position = MoveAndSlide(gt) * SpeedMultiplier;
        HandleStates();
        Velocity = Vector2.Clamp(Velocity, new Vector2(-MaxSpeed, -MaxSpeed), new Vector2(MaxSpeed, MaxSpeed));
    }
    public void Draw(SpriteBatch batch) 
    {
        PlayerSprite.Draw(batch, Bounds);
    }
    
}