using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public class SceneManager : GameManager 
{
    public enum UI_States 
    {
        MainMenu,
        Settings
    }
    public enum GameStates 
    {
        Alive,
        GameOver
    }
    protected UI_States InterfaceState;
    protected GameStates GameState;
    public UI_States SwitchInterface(UI_States newUI) => InterfaceState = newUI;
    public GameStates SwitchGameState(GameStates newGameState) => GameState = newGameState;
    public GameLogicManager GameManager { get; set; }
    public MainUI InterfaceManager { get; set; }
    public Camera Camera { get; private set; }
    public Player Player;
    private Matrix TrueMatrix = new();
    public SceneManager(Game game) 
    {
        GameManager = new(game);
        InterfaceManager = new(game);
        Player = new(480, 270, 64, 64, 100);
    }
    public override void Initialize(Game game) 
    {
        GameManager.Initialize(game);
        InterfaceManager.Initialize(game);
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        GameManager.LoadContent(device, manager);
        Player.LoadContent(manager);
        InterfaceManager.LoadContent(device, manager);

        Camera = new(device.Viewport.Bounds);
        Camera.SwitchStates(CameraStates.Lerped);
    }
    public override void UpdateLogic(GameTime gt) 
    {
        GameManager.UpdateLogic(gt);
        Player.UpdateLogic(gt);
        InterfaceManager.UpdateLogic(gt);

        Camera.SetTarget(Player.Center);

        TrueMatrix = Camera.RotationMatrix * Camera.ScaleMatrix * Camera.TransformMatrix;
    }
    public override void Draw(SpriteBatch batch) 
    {
        GameManager.Draw(batch, Player, TrueMatrix);
        InterfaceManager.Draw(batch);
    }
    
}