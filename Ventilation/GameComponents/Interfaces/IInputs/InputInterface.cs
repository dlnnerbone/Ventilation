
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Interfaces;
public interface IInputs 
{
    public KeyboardState CurrentKeyboardState { get; protected set; }
    public KeyboardState PreviousKeyboardState { get; protected set; }
    
}