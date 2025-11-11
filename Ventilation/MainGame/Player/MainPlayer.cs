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
    public CharacterMovementModule Movement { get; private set; }
    
    public Animation PlayerIdleAnimation { get; private set; }
    public TextureAtlas IdleAtlas { get; private set; }

    private WebClump clump = new(16, 16, Vector2.UnitX, Actions.Ready);
    
    public Player(int x, int y, int width = 64, int height = 64, float HP = 100) : base(x, y, width, height, HP)
    {
        Size *= 4;
        clump.Size *= 4;
    }
    public void LoadPlayerContent(ContentManager content) 
    {
        IdleAtlas = new TextureAtlas(4, 4, 256, 256);
        PlayerIdleAnimation = new(content.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"), IdleAtlas, 0, 15, 10);
        PlayerIdleAnimation.LayerDepth = 0.5f;
        clump.LoadContent(content);
        
        Movement = new(content);
    }
    public void RollThePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        PlayerIdleAnimation.Advance(gt);
        Movement.UpdateMovement(gt, this);
        clump.ShootingTime(gt);
        clump.SetTarget(Center);

        if (MouseManager.IsLeftHeld)
        {
            clump.LifeSpan.Restart();
            clump.AimAt(MouseManager.WorldMousePosition);
            clump.OverrideFlags(Actions.Ready);
        }
        else if (!clump.InCooldown)
        {
            clump.OverrideFlags(Actions.Active);
        }
        if (clump.LifeSpan.TimeHitsFloor()) 
        {
            clump.OverrideFlags(Actions.Cooldown);
        }
        
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        PlayerIdleAnimation.Animate(batch, Bounds);
        batch.Draw(PlayerIdleAnimation.SpriteSheet, new Rectangle(0, 0, 100, 100), Color.White);
        clump.DrawProjectile(batch);
    }
    public void DrawPlayerStats(SpriteBatch batch) 
    {
        Movement.DisplayPlayerMovementStats(batch);
    }
}