using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents.Interfaces;
namespace GameComponents.Managers;
public sealed class InputManager : IInputs
{
    public KeyboardState CurrentKeyboardState { get; set; }
    public KeyboardState PreviousKeyboardState { get; set; }
    public MouseState CurrentMouseState { get; set; }
    public MouseState PreviousMouseState { get; set; }
    public bool WASD => IsKeyDown(Keys.W) || IsKeyDown(Keys.A) || IsKeyDown(Keys.S) || IsKeyDown(Keys.D);
    public bool ArrowKeys => IsKeyDown(Keys.Up) || IsKeyDown(Keys.Down) || IsKeyDown(Keys.Left) || IsKeyDown(Keys.Right);
    public bool IsKeyDown(Keys key) 
    {
        return CurrentKeyboardState.IsKeyDown(key);
    }
    public bool IsKeyPressed(Keys key) 
    {
        return CurrentKeyboardState.IsKeyDown(key) && !PreviousKeyboardState.IsKeyDown(key);
    }
    public bool IsKeyReleased(Keys key) 
    {
        return !CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyDown(key);
    }
    public void UpdateInputs() 
    {
        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = Keyboard.GetState();
    }
}