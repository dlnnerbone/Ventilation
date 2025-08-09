
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Interfaces;
public interface InterfaceInputs 
{
    public KeyboardState CurrentKeyboardState { get; protected set; }
    public KeyboardState PreviousKeyboardState { get; protected set; }
    
}