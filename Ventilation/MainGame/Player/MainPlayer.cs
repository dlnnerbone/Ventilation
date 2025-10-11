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
    // the rest of the stuff specific to player
    public Animation PlayerIdleAnimation { get; private set; }
    public TextureAtlas IdleAtlas { get; private set; }
    public Player(int x, int y, int width = 64, int height = 64, float HP = 100) : base(x, y, width, height, HP)
    {
        
    }
    public void LoadPlayerContent(ContentManager content, GraphicsDevice device) 
    {
        IdleAtlas = new(content.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"), 4, 4);
        
        PlayerIdleAnimation = new(IdleAtlas, 0, 15);
        PlayerIdleAnimation.FPS = 10;
        
        pMovement = new(content);
    }
    public void RollThePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        PlayerIdleAnimation.Roll(gt);
        pMovement.UpdateMovement(gt, this);
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        PlayerIdleAnimation.Scroll(batch, Bounds, Color.White);
    }
    public void DrawPlayerStats(SpriteBatch batch) 
    {
        pMovement.DisplayPlayerMovementStats(batch);
    }
}