using Microsoft.Xna.Framework;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents;
namespace Main;
public sealed class Player : Entity
{
    public PlayerMovement Movement { get; set; }
    // the rest of the stuff specific to player
    
    public Animation PlayerIdleAnimation { get; private set; }
    public TextureAtlas IdleAtlas { get; private set; }
    
    private WebClump webClump = new();
    public Player(int x, int y, int width = 196, int height = 196, float HP = 100) : base(x, y, width, height, HP) 
    {
        webClump.OverrideFlags(Actions.Ready);
    }
    public void LoadPlayerContent(ContentManager content) 
    {
        IdleAtlas = new TextureAtlas(4, 4, 256, 256);
        PlayerIdleAnimation = new(content.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"), IdleAtlas, 0, 15);
        PlayerIdleAnimation.FPS = 10;

        webClump.LoadContent(content);
        Movement = new(content);
    }
    public void RollThePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        PlayerIdleAnimation.Roll(gt);
        Movement.UpdateMovement(gt, this);
        
        webClump.ShootingTime(gt, this);
        if (MouseManager.IsRightClicked) webClump.OverrideFlags(Actions.Active);
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        PlayerIdleAnimation.Scroll(batch, Bounds);
        batch.Draw(PlayerIdleAnimation.SpriteSheet, new Rectangle(0, 0, 100, 100), Color.White);
        webClump.DrawProjectile(batch);
    }
    public void DrawPlayerStats(SpriteBatch batch) 
    {
        Movement.DisplayPlayerMovementStats(batch);
    }
}