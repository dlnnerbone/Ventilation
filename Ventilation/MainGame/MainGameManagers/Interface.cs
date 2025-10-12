using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Managers;
namespace Main;
public sealed class Interface : Scene 
{
    public Interface(string name = "MainUI") : base(name) {}
    public override void Initialize(Game game) 
    {
        base.Initialize(game);
    }
    public override void LoadSceneContent(Game game, string rootDir = "Content") 
    {
        base.LoadSceneContent(game, rootDir);
    }
    public override void UpdateScene(GameTime gt) 
    {
        base.UpdateScene(gt);
    }
    public void DrawScene(SpriteBatch batch, Player player) 
    {
        DrawScene();
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        player.DrawPlayerStats(batch);
        batch.End();
    }
}