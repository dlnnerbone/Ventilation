using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents.Interfaces;
namespace GameComponents;
public class InputManager : IMouse, InterfaceInputs
{
    public KeyboardState CurrentKeyboardState { get; set; }
    public KeyboardState PreviousKeyboardState { get; set; }
    public MouseState CurrentMouseState { get; set; }
    public MouseState PreviousMouseState { get; set; }
    public Vector2 ScreenMousePosition => new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
    public Vector2 ClientMousePosition { get; private set; }
    public int ScrollWheelData { get; set; }
    public bool IsLeftClicked { get; private set; }
    public bool IsRightClicked { get; private set; }
    public bool IsMiddleClicked { get; private set; }
    public bool IsBackClicked { get; private set; }
    public bool IsForwardClicked { get; private set; }
    public bool IsLeftHeld => CurrentMouseState.LeftButton == ButtonState.Pressed;
    public bool IsRightHeld => CurrentMouseState.RightButton == ButtonState.Pressed;
    public bool IsMiddleHeld => CurrentMouseState.MiddleButton == ButtonState.Pressed;
    public bool IsBackHeld => CurrentMouseState.XButton1 == ButtonState.Pressed;
    public bool IsForwardHeld => CurrentMouseState.XButton2 == ButtonState.Pressed;
    public bool IsKeyDown(Keys key) 
    {
        return CurrentKeyboardState.IsKeyDown(key);
    }
    public void InvertMatrixForMouse(Matrix matrix) 
    {
        ClientMousePosition = Vector2.Transform(ScreenMousePosition, Matrix.Invert(matrix));
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

        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = Mouse.GetState();

        IsLeftClicked = CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;
        IsRightClicked = CurrentMouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released;
        IsMiddleClicked = CurrentMouseState.MiddleButton == ButtonState.Pressed && PreviousMouseState.MiddleButton == ButtonState.Released;

        IsBackClicked = CurrentMouseState.XButton1 == ButtonState.Pressed && PreviousMouseState.XButton1 == ButtonState.Released;
        IsForwardClicked = CurrentMouseState.XButton2 == ButtonState.Pressed && PreviousMouseState.XButton2 == ButtonState.Released;
        ScrollWheelData = CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue;
    }
}