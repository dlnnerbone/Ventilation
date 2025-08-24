using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
namespace Main;

public class GameLogicManager : GameManager
{
    public GameLogicManager(Game game) 
    {
        
    }
    public override void Initialize(Game game) 
    {
        
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        
    }
    public override void UpdateLogic(GameTime GT) 
    {
        
    }
    public void Draw(SpriteBatch batch, Player Player, Matrix matrix) 
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, matrix);
        Player.Draw(batch);
        batch.End();
    }
}