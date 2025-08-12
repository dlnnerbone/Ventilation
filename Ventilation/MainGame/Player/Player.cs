using Microsoft.Xna.Framework;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public class Player : Entity 
{
    private InputManager Input = new();
    private int stamina = 3;
    private float moveSpeed = 50f;
    private Actions ActionState = Actions.Ready;
    private Motions MotionState = Motions.Idle;
    private int maxStamina = 3;
    // private fields
    public Sprite PlayerSprite { get; set; }
    public int Stamina { get { return stamina; } set { stamina = MathHelper.Clamp(value, 0, maxStamina); } }
    
}