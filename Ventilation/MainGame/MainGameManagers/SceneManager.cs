using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public sealed class SceneManager : Scene 
{
    public Camera MainCamera { get; private set; }
    // base methods
    public SceneManager(string name) : base(name) {}
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
        MainCamera = new(game.GraphicsDevice.Viewport, true);
    }
    public override void LoadSceneContent(Game game, string dir = "Content/GameAssets") 
    {
        base.LoadSceneContent(game, dir);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
        MainCamera.Recording();
        MainCamera.SetTarget(new Vector2(910, 540));
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();

        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, MainCamera.WorldMatrix);
        batch.End();
        
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        batch.End();
    }
}