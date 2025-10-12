using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
using GameComponents.Managers;
namespace Main;
public sealed class SceneManager : Scene 
{
    public readonly GameManager GameManager = new();
    public readonly Interface Interface = new();
    public Player MainPlayer => GameManager.Player;
    // base methods
    public SceneManager(string name) : base(name) {}
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
        GameManager.Initialize(game);
        Interface.Initialize(game);
    }
    public override void LoadSceneContent(Game game, string dir = "Content") 
    {
        base.LoadSceneContent(game, dir);
        GameManager.LoadSceneContent(game);
        Interface.LoadSceneContent(game);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
        MouseManager.UpdateInputs();
        MouseManager.TransformWorldPosition(GameManager.MainCamera.WorldMatrix);
        
        GameManager.UpdateScene(gt);
        Interface.UpdateScene(gt);
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();
        GameManager.DrawScene(batch);
        Interface.DrawScene(batch, MainPlayer);
    }
}