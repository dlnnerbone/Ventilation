using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class MainUI : GameManager 
{
    public MainUI(Game game) 
    {
        
    }
    public override void Initialize(Game game) 
    {
        
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        
    }
    public override void UpdateLogic(GameTime gt) 
    {
        
    }
    public override void Draw(SpriteBatch batch) 
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        batch.End();
    }
}