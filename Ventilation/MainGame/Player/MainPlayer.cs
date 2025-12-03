using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Helpers;
using Microsoft.Xna.Framework.Input;
using GameComponents.Managers;
namespace Main;

public sealed class Player : Entity
{
    public CharacterMovementModule Movement { get; set; }
    public PlayerCombatModule combatModule { get; private set; }
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.White);
        
        combatModule = new(content);
    }
    
    protected override void MoveAndSlide(GameTime gt) => Position += Velocity * (float)gt.ElapsedGameTime.TotalSeconds;
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        MoveAndSlide(gt);
        
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
    }
}