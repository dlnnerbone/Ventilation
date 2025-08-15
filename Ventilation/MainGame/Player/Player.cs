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
    private PlayerMotion playerMovement;
    // private fields
    public bool IsAlive { get; set; } = true;
    public bool IsControllable { get; set; } = true;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        playerMovement = new();
    }
    public void LoadContent(GraphicsDevice device) 
    {
        
    }
    public void UpdateLogic(GameTime gt) 
    {
        playerMovement.HandleMotionStates(this);
    }
    public void Draw(SpriteBatch batch) 
    {
        
    }
    
}