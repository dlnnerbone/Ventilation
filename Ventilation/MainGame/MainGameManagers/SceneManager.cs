using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public sealed class SceneManager : Scene 
{
    public Camera MainCamera { get; private set; }
    public Player MainPlayer { get; set; } = new(50, 500, 64, 64);
    // base methods
    public SceneManager(string name) : base(name) {}
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
        MainCamera = new(game.GraphicsDevice.Viewport, true, 1, 0.1f, 2, 0.4f);
    }
    public override void LoadSceneContent(Game game, string dir = "Content") 
    {
        base.LoadSceneContent(game, dir);
        MainPlayer.LoadPlayerContent(SceneContent, game.GraphicsDevice);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
        MainPlayer.RollThePlayer(gt);
        MainCamera.Recording();
        MainCamera.SetTarget(-MainPlayer.Center);
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();

        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, MainCamera.WorldMatrix);
        MainPlayer.DrawPlayer(batch);
        batch.Draw(MainPlayer.Sprite.Texture, new Rectangle(500, 500, 100, 100), Color.White);
        batch.End();
        
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        MainPlayer.DrawPlayerStats(batch);
        batch.End();
    }
}