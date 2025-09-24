using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Main;
public class MainUI : GameManager 
{
    private InputManager input = new();
    private float fps;
    private SpriteFont spriteFont;
    public MainUI(Game game) 
    {
        fps = 1;
    }
    public override void Initialize(Game game) 
    {
        
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        spriteFont = manager.Load<SpriteFont>("GameAssets/SpriteFonts/PixelatedElegance");
    }
    public override void UpdateLogic(GameTime gt) 
    {
        input.UpdateInputs();
        fps = 1 / (float)gt.ElapsedGameTime.TotalSeconds;
    }
    public override void Draw(SpriteBatch batch) 
    {
        if (input.IsKeyDown(Keys.Tab))
        {
            batch.DrawString(spriteFont, $"{fps}", new Vector2(1500, 50), Color.White);
        }
    }
}