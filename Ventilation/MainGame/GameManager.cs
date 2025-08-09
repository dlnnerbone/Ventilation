using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Managers;
namespace Main;

public class GameLogicManager : GameManager
{
    public override void Initialize(Game game)
    {

    }
    public override void LoadContent(Game game)
    {

    }
    public override void UpdateLogic(GameTime gt)
    {

    }
    public override void Draw(SpriteBatch batch)
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
        batch.End();
    }
}