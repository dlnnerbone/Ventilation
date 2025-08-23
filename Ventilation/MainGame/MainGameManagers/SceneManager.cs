using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        GameOver
    }
    public UI_States InterfaceState;
    public GameManager GameManager;
    public MainUI InterfaceManager;
    public PlayerManager PlayerManager;
    
}