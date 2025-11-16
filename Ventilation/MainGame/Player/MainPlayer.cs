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
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    
    WebClump web = new();
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.White);
        web.LoadContent(content);
    }
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        MoveAndSlide(gt);
        Movement.UpdateMovement(gt, this);
        web.ShootingTime(gt);
        
        if (MouseManager.IsLeftHeld) 
        {
            web.SetDestination(Center);
            web.AimAt(MouseManager.WorldMousePosition);
            web.OverrideFlags(Actions.Ready);
        } 
        else 
        {
            web.OverrideFlags(Actions.Active);
        }
    }
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        web.DrawProjectile(spriteBatch);
    }
}