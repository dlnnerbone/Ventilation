using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
using GameComponents.Managers;
namespace Main;
public sealed class SceneManager : Scene 
{
    public readonly GameManager GameManager = new();
    public Player MainPlayer => GameManager.Player;
    // base methods
    public SceneManager(string name) : base(name) {}
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
        GameManager.Initialize(game);
    }
    public override void LoadSceneContent(Game game, string dir = "Content") 
    {
        base.LoadSceneContent(game, dir);
        GameManager.LoadSceneContent(game);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
        MouseManager.UpdateInputs();
        MouseManager.TransformWorldPosition(GameManager.MainCamera.WorldMatrix);
        Diagnostics.Write($"Pos: {MouseManager.WorldMousePosition.X}, {MouseManager.WorldMousePosition.Y}");
        GameManager.UpdateScene(gt);
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();
        GameManager.DrawScene(batch);
    }
}