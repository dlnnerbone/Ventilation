using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
using GameComponents;
namespace Main;
public sealed class SceneManager : Scene 
{

    public SceneManager(string name) : base(name) {}
    
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
    }
    public override void LoadSceneContent(Game game, string dir = "Content/GameAssets") 
    {
        base.LoadSceneContent(game, dir);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
    }
    public void DrawScene(SpriteBatch batch) 
    {
        DrawScene();
        batch.Begin();
        batch.End();
    }
}