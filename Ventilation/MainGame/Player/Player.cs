using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Logic;
namespace Main;
public class Player : Entity 
{
    private InputManager Input = new();
    private float moveSpeed = 50f;
    private float dashForce = 2000f;
    private int stamina = 3;
    private float speedMultiplier = 1;
    private Actions ActionState = Actions.Ready;
    private Motions MotionState = Motions.Idle;
    private Timer dashCool, dashDur, staminaRegen, attackCool;
    // private fields
    
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        dashCool = new(0.4f);

        dashDur = new(0.2f);

        staminaRegen = new(1.5f);
        staminaRegen.AutoRestartOnZero();

        attackCool = new(0.65f);
        
    }
    public void LoadContent(GraphicsDevice device) 
    {
        
    }
    public void UpdateLogic(GameTime gt) 
    {
        MoveAndSlide(gt);
        Input.UpdateInputs();
    }
    public void Draw(SpriteBatch batch) 
    {
        
    }
    
}