using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public class PlayerMovement 
{
    private Timer dashCool, dashDur, staminaCool;
    private InputManager input = new();
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float dashForce = 2000f;
    private float speedMultiplier = 1f;
    private float stamina = 100f;
    private float maxStamina = 100f;
}
