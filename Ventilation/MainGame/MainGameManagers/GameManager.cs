using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Managers;
using GameComponents.Logic;
using Microsoft.Xna.Framework.Content;
namespace Main;
public sealed class GameManager : Scene 
{
    public Camera MainCamera { get; set; }
    public Player Player { get; set; }
    
    public GameManager(string name = "GameLogicManager") : base(name) 
    {
        Player = new Player();
    }
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
        MainCamera = new(game.GraphicsDevice.Viewport, true);
        MainCamera.EaseLevel = 0.35f;
    }
    public override void LoadSceneContent(Game game, string contentDir = "Content")
    {
        base.LoadSceneContent(game, contentDir);
        Player.LoadContent(game.GraphicsDevice, SceneContent);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
        Player.UpdatePlayer(gt);
        MainCamera.Recording();
        MainCamera.SetTarget(-Player.Center);
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);
        Player.DrawPlayer(batch);
        batch.End();
    }
    
}