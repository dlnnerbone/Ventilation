using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Managers;
using GameComponents.Logic;
namespace Main;

public class GameLogicManager : GameManager
{
    private Camera GameCamera;
    private Matrix GameMatrix;
    public override void Initialize(Game game)
    {
        GameCamera = new();
        GameCamera.CreateScreenMatch(game.GraphicsDevice.PresentationParameters.Bounds);
    }
    public override void LoadContent(Game game)
    {

    }
    public override void UpdateLogic(GameTime gt)
    {
        GameCamera.UpdateCamera(gt);
        GameMatrix = GameCamera.ScaleMatrix * GameCamera.RotationMatrix * GameCamera.TransformMatrix;
    }
    public override void Draw(SpriteBatch batch)
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, GameMatrix);
        batch.End();
    }
}