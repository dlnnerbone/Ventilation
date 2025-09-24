using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public class SceneManager : GameManager 
{
    public enum UI_States  { MainMenu, Settings }
    public enum GameStates { Alive, GameOver }
    
    protected UI_States InterfaceState;
    protected GameStates GameState;
    
    public UI_States SwitchInterface(UI_States newUI) => InterfaceState = newUI;
    public GameStates SwitchGameState(GameStates newGameState) => GameState = newGameState;
    
    public GameLogicManager GameManager { get; private set; }
    public MainUI InterfaceManager { get; private set; }
    
    public SceneManager(Game game) 
    {
        GameManager = new(game);
        InterfaceManager = new(game);
    }
    public override void Initialize(Game game) 
    {
        GameManager.Initialize(game);
        InterfaceManager.Initialize(game);
    }
    public override void LoadContent(GraphicsDevice device, ContentManager manager) 
    {
        GameManager.LoadContent(device, manager);
        InterfaceManager.LoadContent(device, manager);
    }
    public override void UpdateLogic(GameTime gt) 
    {
        MouseManager.UpdateInputs();
        GameManager.UpdateLogic(gt);
        InterfaceManager.UpdateLogic(gt);
    }
    public override void Draw(SpriteBatch batch) 
    {
        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);
        GameManager.Draw(batch);
        batch.End();

        batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, Matrix.Identity);
        InterfaceManager.Draw(batch);
        batch.End();
        
    }
    
}