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
    private float moveSpeed = 500f;
    private float dashForce = 2000f;
    private int stamina = 3;
    private float speedMultiplier = 1;
    private Timer dashDur, dashCooldown, staminaRegen, attackCooldown;
    
}