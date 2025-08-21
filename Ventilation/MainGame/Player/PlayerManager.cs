using GameComponents.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class PlayerManager : GameManager 
{
    public Player player { get; private set; }
    public PlayerManager() 
    {
        player = new(250, 250, 128, 128, 100);
    }
    public override void Initialize(Game game) {}
    public override void LoadContent(Game game) 
    {
        player.LoadContent(game);
    }
    public override void UpdateLogic(GameTime gt) 
    {
        player.UpdateLogic(gt);
    }
    public override void Draw(SpriteBatch batch) 
    {
        player.Draw(batch);
    }
}