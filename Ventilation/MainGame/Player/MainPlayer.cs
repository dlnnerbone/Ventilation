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
    
    // the rest of the stuff specific to player
    public Sprite Sprite { get; private set; }
    public Player(int x, int y, int width = 64, int height = 64, float HP = 100) : base(x, y, width, height, HP)
    {
        
    }
    public void LoadPlayerContent(ContentManager content, GraphicsDevice device) 
    {
        Sprite = new(new Texture2D(device, 1, 1), Color.White);
        Sprite.SetToData();
        
        pMovement = new(content);
    }
    public void RollThePlayer(GameTime gt) 
    {
        MoveAndSlide(gt);
        pMovement.UpdateMovement(gt, this);
    }
    public void DrawPlayer(SpriteBatch batch) 
    {
        Sprite.Draw(batch, Bounds);
        pMovement.DisplayPlayerMovementStats(batch);
    }
}