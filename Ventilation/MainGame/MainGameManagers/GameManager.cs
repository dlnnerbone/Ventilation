using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
namespace Main;

public class GameLogicManager : GameManager
{
    private Rectangle TestBounds;
    private Texture2D TestTexture;
    private Color[] TestColor = new Color[] { Color.White };

    public Player Player;
    public GameLogicManager(Game game) 
    {
        TestBounds = new(500, 500, 64, 64);
        Player = new(500, 500);
    }
    public override void Initialize(Game game) 
    {
        
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        TestTexture = new(device, 1, 1);
        TestTexture.SetData<Color>(TestColor);
        Player.LoadPlayerContent(manager, device);
    }
    public override void UpdateLogic(GameTime GT) 
    {
        Player.RollThePlayer(GT);
    }
    public override void Draw(SpriteBatch batch) 
    {
        batch.Draw(TestTexture, TestBounds, Color.White);
        Player.DrawPlayer(batch);
    }
}