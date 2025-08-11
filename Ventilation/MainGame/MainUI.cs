using Microsoft.Xna.Framework;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class MainUI : GameManager 
{
    
    public override void Initialize(Game game) 
    {
        
    }
    public override void LoadContent(Game game) 
    {
        
    }
    public override void UpdateLogic(GameTime GT) 
    {
        
    }
    public override void Draw(SpriteBatch batch) 
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        batch.End();
    }
}