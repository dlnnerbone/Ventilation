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
        WindowManager.ChangeResolution(Device, 1920, 1080);
        WindowManager.SetTitle(this, "breath in the dusty air!");

        Content.RootDirectory = "Content";
        MainScene = new(this);
        IsMouseVisible = true;
    }
    protected override void Initialize() 
    {
        base.Initialize();
        MainScene.Initialize(this);
    }
    protected override void LoadContent() 
    {
        base.LoadContent();

        SpriteBatch = new(GraphicsDevice);
        Canvas = new(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        MainScene.LoadContent(GraphicsDevice, Content);
    }
    protected override void Update(GameTime gameTime) 
    {
        base.Update(gameTime);
        MainScene.UpdateLogic(gameTime);
    }
    protected override void Draw(GameTime gameTime) 
    {
        GraphicsDevice.Clear(Color.Black);
        
        MainScene.Draw(SpriteBatch);

        base.Draw(gameTime);
    }
    
}