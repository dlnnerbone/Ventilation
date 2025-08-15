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
    private PlayerMotion playerMovement = new();
    // private fields
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    public bool IsControllable { get; set; } = true;
    public float SpeedMultiplier { get; set; } = 1;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        
    }
    public void LoadContent(GraphicsDevice device) 
    {
        PlayerSprite = new(new Texture2D(device, 1, 1), Color.Red);
        PlayerSprite.SetToData();
    }
    public void UpdateLogic(GameTime gt) 
    {
        MoveAndSlide(gt);
        playerMovement.HandleMotionStates(this);
    }
    public void Draw(SpriteBatch batch) 
    {
        PlayerSprite.Draw(batch, Bounds);
    }
    
}