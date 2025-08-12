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
    private int stamina = 3;
    private float moveSpeed = 50f;
    private float dashForce = 2000f;
    private Actions ActionState = Actions.Ready;
    private Motions MotionState = Motions.Idle;
    private bool isDashing = false;
    private bool canDash = true;
    private int maxStamina = 3;
    // private fields
    public Sprite PlayerSprite { get; set; }
    private Timer DashCooldown, DashDuration, StaminaRegen, AttackCooldown;
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    public bool IsDashing { get { return isDashing; } set { isDashing = canDash == false ? false : value; } }
    public bool CanDash { get { return canDash; } private set { canDash = value; } }
    public float DashForce { get { return dashForce; } set { dashForce = value; } }
    public float MaxSpeed { get; set; } = 500f;
    public Player(Vector2 position, int width, int height, float HP) : base(position, new Vector2(width, height), HP) 
    {
        DashCooldown = new(0.3f);
        DashCooldown.DisableAutoRestart();

        DashDuration = new(0.2f);
        DashDuration.DisableAutoRestart();

        StaminaRegen = new(1.5f);
        StaminaRegen.AutoRestartOnZero();

        AttackCooldown = new(0.45f);
        AttackCooldown.DisableAutoRestart();
    }
    private void MovementInputStates() {}
    private void ActionInputStates() {}
    private void MotionStating() {}
    private void ActionStating() {}
    private void Idle() 
    {
        Velocity = Vector2.Lerp(Velocity, Vector2.Zero, 0.6f);
        IsDashing = false;
        Velocity_X = Velocity_X <= 1 || Velocity_X >= -1 ? 0 : Velocity_X;
        Velocity_Y = Velocity_Y <= 1 || Velocity_Y >= -1 ? 0 : Velocity_Y;
    }
    private void Moving() 
    {
        Velocity = Vector2.Zero;
        IsDashing = false;
        if (Input.IsKeyDown(Keys.W)) Velocity_Y += -moveSpeed;
        else if (Input.IsKeyDown(Keys.S)) Velocity_Y += moveSpeed;
        if (Input.IsKeyDown(Keys.A)) Velocity_X += -moveSpeed;
        else if (Input.IsKeyDown(Keys.D)) Velocity_X += moveSpeed;
    }
    private void Dashing() 
    {
        Velocity += Direction * DashForce;
        if (DashDuration.ElapsedTime <= 0) 
        {
            IsDashing = false;
        }
    }
    public void LoadContent(GraphicsDevice device) 
    {
        PlayerSprite = new(new Texture2D(device, 1, 1), Color.Red);
        PlayerSprite.SetToData();
    }
    public void UpdatePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        Velocity_Y = MathHelper.Clamp(Velocity_Y, -MaxSpeed, MaxSpeed);
        Velocity_X = MathHelper.Clamp(Velocity_X, -MaxSpeed, MaxSpeed);
    }
    public void Draw(SpriteBatch batch) 
    {
        PlayerSprite.Draw(batch, Bounds);
    }
    
}