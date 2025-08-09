using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Interfaces;
public interface IMouse 
{
    public MouseState CurrentMouseState { get; protected set; }
    public MouseState PreviousMouseState { get; protected set; }
    public int ScrollWheelData { get; protected set; }
}