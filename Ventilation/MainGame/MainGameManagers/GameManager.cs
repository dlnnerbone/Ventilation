using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Managers;
using GameComponents.Logic;
namespace Main;

public class GameLogicManager : GameManager
{
    private Camera GameCamera;
    private Matrix GameMatrix;
    private PlayerManager playerManager;
    public GameLogicManager(Game game) 
    {
        GameCamera = new();
        playerManager = new();
        GameCamera.CreateScreenMatch(game.GraphicsDevice.PresentationParameters.Bounds);
    }
    public override void Initialize(Game game)
    {
        playerManager.Initialize(game);
    }
    public override void LoadContent(Game game)
    {
        playerManager.LoadContent(game);
    }
    public override void UpdateLogic(GameTime gt)
    {
        GameCamera.UpdateCamera(gt);
        playerManager.UpdateLogic(gt);
        
        GameMatrix = GameCamera.ScaleMatrix * GameCamera.RotationMatrix * GameCamera.TransformMatrix;
    }
    public override void Draw(SpriteBatch batch)
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, GameCamera.TransformMatrix);
        playerManager.Draw(batch);
        batch.End();
    }
}