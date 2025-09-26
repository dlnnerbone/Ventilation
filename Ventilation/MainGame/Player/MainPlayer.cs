using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents;
namespace Main;
public sealed class Player : Entity
{
    private PlayerMovement pMovement;
    // public properties (movement)
    public float MoveSpeed { get => pMovement.MoveSpeed; set => pMovement.MoveSpeed = value; }
    public float SpeedMultiplier { get => pMovement.SpeedMultiplier; set => pMovement.SpeedMultiplier = value; }
    public float DashForce { get => pMovement.DashForce; set => pMovement.DashForce = value; }
    public float Stamina { get => pMovement.Stamina; set => pMovement.Stamina = value; }
    public float MaxStamina { get => pMovement.MaxStamina; set => pMovement.MaxStamina = value; }
    public bool IsDashing { get => pMovement.IsDashing; set => pMovement.IsDashing = value; }
    public bool CanDash { get => pMovement.CanDash; set => pMovement.CanDash = value; }
    public bool IsControllable { get => pMovement.IsControllable; set => pMovement.IsControllable = value; }
    public void StaminaIntake(float intakeAmount) => pMovement.StaminaIntake(intakeAmount);
    public Motions MotionState { get => pMovement.MotionState; set => pMovement.MotionState = value; }
    // the rest of the stuff specific to player
    public Sprite Sprite { get; private set; }
    public Player(int x, int y, int width = 64, int height = 64, float HP = 100) : base(x, y, width, height, HP) 
    {
        pMovement = new();
    }
    public void LoadPlayerContent(ContentManager content, GraphicsDevice device) 
    {
        Sprite = new(new Texture2D(device, 1, 1), Color.White);
        Sprite.SetToData();
    }
    public void RollThePlayer(GameTime gt) 
    {
        pMovement.UpdateMovement(gt, this);
        MoveAndSlide(gt);
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        Sprite.Draw(batch, Bounds);
    }
}