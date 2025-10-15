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
    public float MaxSpeed { get => pMovement.MaxSpeed; set => pMovement.MaxSpeed = value; }
    public float SpeedMulti { get => pMovement.SpeedMulti; set => pMovement.SpeedMulti = value; }
    public float DashForce { get => pMovement.DashForce; set => pMovement.DashForce = value; }
    public float Stamina { get => pMovement.Stamina; set => pMovement.Stamina = value; }
    public float MaxStamina { get => pMovement.MaxStamina; set => pMovement.MaxStamina = value; }
    public float EaseLevel { get => pMovement.EaseLevel; set => pMovement.EaseLevel = value; }
    public bool CanDash { get => pMovement.CanDash; set => pMovement.CanDash = value; }
    public bool IsDashing => pMovement.IsDashing;
    public bool IsControllable { get => pMovement.IsControllable; set => pMovement.IsControllable = value; }
    public bool IsMotionActive { get => pMovement.IsActive; set => pMovement.IsActive = value; }
    // the rest of the stuff specific to player
    public Animation PlayerIdleAnimation { get; private set; }
    private Sprite _playerSprite;
    public TextureAtlas IdleAtlas { get; private set; }
    public Player(int x, int y, int width = 64, int height = 64, float HP = 100) : base(x, y, width, height, HP) {}
    public void LoadPlayerContent(ContentManager content, GraphicsDevice device) 
    {
        _playerSprite = new(content.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"));
        IdleAtlas = new(_playerSprite, 4, 4);
        PlayerIdleAnimation = new(IdleAtlas, 0, 15);
        PlayerIdleAnimation.FPS = 10;
        _playerSprite.Origin = PlayerIdleAnimation.GetFrameOrigin();
        _playerSprite.Direction = Vector2.One;
        _playerSprite.Color = Color.White;
        
        pMovement = new(content);
    }
    public void RollThePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        PlayerIdleAnimation.Roll(gt);
        pMovement.UpdateMovement(gt, this);

        _playerSprite.Direction = Vector2.Zero - Position;
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        PlayerIdleAnimation.Scroll(batch, Bounds, _playerSprite);
        batch.Draw(_playerSprite.Texture, new Rectangle(0, 0, 100, 100), Color.White);
    }
    public void DrawPlayerStats(SpriteBatch batch) 
    {
        pMovement.DisplayPlayerMovementStats(batch);
    }
}