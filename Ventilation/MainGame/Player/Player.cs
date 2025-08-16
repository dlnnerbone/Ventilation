using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Managers;
using GameComponents.Logic;
namespace Main;
public class Player : Entity 
{
    private Motions Motion = Motions.Idle;
    public InputManager Input = new();
    private float moveSpeed = 50f;
    private float maxSpeed = 750f;
    private float lerpSpeed = 0.15f;
    private float dashForce = 2000f;
    private Timer dashCool, dashDur, staminaRegen;
    private int stamina = 3;
    // private fields for movement utilities
    public Motions SetMotion(Motions motion) => Motion = motion;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value < 0 ? 0 : value; } }
    public float LerpSpeed { get { return lerpSpeed; } set { lerpSpeed = MathHelper.Clamp(value, 0f, 1f); } }
    public float DashForce { get { return dashForce; } set { dashForce = value <= maxSpeed ? value * 2 : value; } }
    public bool IsDashing { get; set; } = false;
    // player related variables
}