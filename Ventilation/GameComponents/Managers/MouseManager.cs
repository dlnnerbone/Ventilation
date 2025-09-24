using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Managers;
public static class MouseManager
{
    public static MouseState CurrentMouseState { get; private set; }
    public static MouseState PreviousMouseState { get; private set; }
    public static Vector2 WorldMousePosition { get; private set; }
    public static Vector2 ScreenMousePosition => new Vector2(CurrentMouseState.Position.X, CurrentMouseState.Position.Y);
    public static bool IsLeftClicked { get; private set; }
    public static bool IsRightClicked { get; private set; }
    public static bool IsBackClicked { get; private set; }
    public static bool IsForwardClicked { get; private set; }
    public static bool IsMiddleClicked { get; private set; }
    public static bool IsLeftHeld => CurrentMouseState.LeftButton == ButtonState.Pressed;
    public static bool IsRightHeld => CurrentMouseState.RightButton == ButtonState.Pressed;
    public static bool IsBackHeld => CurrentMouseState.XButton1 == ButtonState.Pressed;
    public static bool IsForwardHeld => CurrentMouseState.XButton2 == ButtonState.Pressed;
    public static bool IsMiddleHeld => CurrentMouseState.MiddleButton == ButtonState.Pressed;
    public static float ScrollWheelValue { get; private set; }
    public static float ScrollWheelValueDelta { get; private set; }
    public static void TransformWorldPosition(Matrix transformMatrix, bool invertMatrix = true) 
    {
        WorldMousePosition = Vector2.Transform(ScreenMousePosition, invertMatrix ? Matrix.Invert(transformMatrix) : transformMatrix);
    }
    public static void UpdateInputs() 
    {
        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = Mouse.GetState();

        IsLeftClicked = CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;
        IsRightClicked = CurrentMouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released;
        IsMiddleClicked = CurrentMouseState.MiddleButton == ButtonState.Pressed && PreviousMouseState.MiddleButton == ButtonState.Released;
        IsBackClicked = CurrentMouseState.XButton1 == ButtonState.Pressed && PreviousMouseState.XButton1 == ButtonState.Released;
        IsForwardClicked = CurrentMouseState.XButton2 == ButtonState.Pressed && PreviousMouseState.XButton2 == ButtonState.Released;

        ScrollWheelValue = CurrentMouseState.ScrollWheelValue;
        ScrollWheelValueDelta = CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue;
        
    }
}