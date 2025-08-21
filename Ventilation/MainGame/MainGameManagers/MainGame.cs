using GameComponents.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class Ventilation : Game 
{
    private SpriteBatch batch;
    private PlayerManager PlayerManager;
    private GraphicsDeviceManager device;
    private GameLogicManager gameManager;
    private MainUI uiManager;
    public Ventilation() 
    {
        device = new(this);
        WindowManager.ChangeResolution(device, 1920, 1080);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        gameManager = new(this);
        uiManager = new(this);
        PlayerManager = new();
    }
    protected override void Initialize()
    {
        base.Initialize();
        PlayerManager.Initialize(this);
        gameManager.Initialize(this);
        uiManager.Initialize(this);
    }
    protected override void LoadContent()
    {
        batch = new(GraphicsDevice);
        gameManager.LoadContent(this);
        uiManager.LoadContent(this);
        PlayerManager.LoadContent(this);
        base.LoadContent();
    }
    protected override void Update(GameTime gt) 
    {
        gameManager.UpdateLogic(gt);
        PlayerManager.UpdateLogic(gt);
        uiManager.UpdateLogic(gt);
        base.Update(gt);
    }
    protected override void Draw(GameTime gt) 
    {
        GraphicsDevice.Clear(Color.Transparent);
        gameManager.Draw(batch);
        uiManager.Draw(batch);
        base.Draw(gt);
    }
    
}