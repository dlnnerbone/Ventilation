using GameComponents.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class Ventilation : Game 
{
    private SpriteBatch batch;
    private GraphicsDeviceManager device;
    public Ventilation() 
    {
        device = new(this);
        WindowManager.ChangeResolution(device, 1920, 1080);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void LoadContent()
    {
        batch = new(GraphicsDevice);
        base.LoadContent();
    }
    protected override void Update(GameTime gt) 
    {
        base.Update(gt);
    }
    protected override void Draw(GameTime gt) 
    {
        GraphicsDevice.Clear(Color.Transparent);
        batch.Begin();
        batch.End();
        base.Draw(gt);
    }
    
}