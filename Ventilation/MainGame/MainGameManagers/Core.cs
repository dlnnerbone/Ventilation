using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents.Managers;
namespace Main;
public sealed class Core : Game
{
    private SceneManager sceneManager;
    
    private SpriteBatch spriteBatch;
    private GraphicsDeviceManager device;
    private InputManager input = new();
    private Keys keyForExit = Keys.Escape;
    private bool canExitGame = true;
    
    public Core(string title, bool isFullScreen = false, bool isMouseVisible = true, string rootDir = "Content") 
    {
        device = new(this);
        device.PreferredBackBufferHeight = 1080;
        device.PreferredBackBufferWidth = 1920;
        device.ApplyChanges();
        
        Window.Title = title ?? throw new ArgumentNullException($"title of game can not be null. {nameof(title)}");
        device.IsFullScreen = isFullScreen;
        IsMouseVisible = isMouseVisible;
        
        Content.RootDirectory = rootDir;

        sceneManager = new("MainGame");
        
    }
    protected override void Initialize() 
    {
        base.Initialize();
        sceneManager.Initialize(this);
    }
    protected override void LoadContent() 
    {
        base.LoadContent();

        spriteBatch = new(GraphicsDevice);
        sceneManager.LoadSceneContent(this);
    }
    protected override void Update(GameTime gt) 
    {
        input.UpdateInputs();
        if (input.IsKeyPressed(keyForExit) && canExitGame) Exit();

        sceneManager.UpdateScene(gt);
        
        base.Update(gt);
    }
    protected override void Draw(GameTime gt) 
    {
        GraphicsDevice.Clear(Color.Transparent);
        sceneManager.DrawScene(spriteBatch);
        base.Draw(gt);
    }
}