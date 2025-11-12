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
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(new Color[] { Color.White });
    }
    
    public void UpdatePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        Movement.UpdateMovement(gt, this);
    }
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        PlayerSprite.Draw(spriteBatch, Bounds);
    }
}