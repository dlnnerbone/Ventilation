using GameComponents.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class Ventilation : Game 
{
    private GraphicsDeviceManager Device;
    private SpriteBatch SpriteBatch;
    private SceneManager MainScene;
    private RenderTarget2D Canvas;
    public Ventilation() 
    {
        Device = new(this);
        WindowManager.ChangeResolution(Device, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        WindowManager.SetTitle(this, "breath in the dusty air!");

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize() 
    {
        base.Initialize();
    }
    protected override void LoadContent() 
    {
        base.LoadContent();

        SpriteBatch = new(GraphicsDevice);
        Canvas = new(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
    }
    protected override void Update(GameTime gameTime) 
    {
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime) 
    {
        GraphicsDevice.SetRenderTarget(Canvas);
        GraphicsDevice.Clear(Color.Black);

        

        GraphicsDevice.SetRenderTarget(null);
        base.Draw(gameTime);
    }
    
}