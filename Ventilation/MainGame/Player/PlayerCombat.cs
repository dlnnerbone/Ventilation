using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public sealed class PlayerCombat 
{
    public InputManager Input { get; private set; }
    public Timer GeneralCooldown { get; private set; } = new(0.75f);
    
}