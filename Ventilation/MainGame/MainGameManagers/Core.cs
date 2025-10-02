using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents.Managers;
namespace Main;
public sealed class Core : Game
{
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

        Window.Title = title ?? throw new ArgumentNullException($"title of game can not be null.");
        device.IsFullScreen = isFullScreen;
        IsMouseVisible = isMouseVisible;
        
        Content.RootDirectory = rootDir;
    }
    protected override void Initialize() 
    {
        base.Initialize();
    }
    protected override void LoadContent() 
    {
        base.LoadContent();

        spriteBatch = new(GraphicsDevice);
    }
    protected override void Update(GameTime gt) 
    {
        input.UpdateInputs();
        if (input.IsKeyPressed(keyForExit) && canExitGame) Exit();
        base.Update(gt);
    }
    protected override void Draw(GameTime gt) 
    {
        GraphicsDevice.Clear(Color.Green);
        spriteBatch.Begin();
        spriteBatch.End();
        base.Draw(gt);
    }
}