using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Helpers;
using GameComponents.Logic;
using GameComponents.Managers;
using System;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class PlayerMovement 
{
    private Timer dashCool, dashDur, staminaRegen;
    private Motions motionState = Motions.Idle;
    private float moveSpd = 200f, maxSpd = 1000f, speedMulti = 1, dashForce = 2000f, stamina = 100f, maxStamina = 100f, easeLvl = 0.4f;
    
    // public properties
    
    public float MoveSpeed { get => moveSpd; set => moveSpd = value * speedMulti; }
    
    public float MaxSpeed { get => maxSpd; set => maxSpd = value * speedMulti; }
    
    public float SpeedMulti { get => speedMulti; set => speedMulti = Math.Abs(value); }
    
    public float DashForce { get => dashForce; set => dashForce = MathHelper.Clamp(value, MaxSpeed, float.PositiveInfinity) * speedMulti; }
    
    public float Stamina { get => stamina; set => stamina = MathHelper.Clamp(value, 0, MaxStamina); }
    
    public float MaxStamina { get => maxStamina; set => Math.Abs(value); }
    
    public float EaseLevel { get => easeLvl; set => easeLvl = MathHelper.Clamp(value, 0f, 1f); }

    public bool IsControllable { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public bool IsDashing { get; private set; } = false;

    public bool CanDash { get; set; } = true;

    public InputManager Input { get; private set; } = new();

    public void SwitchStates(Motions newState) => motionState = newState;
    // switch methods
    private void Idle(Player player) 
    {
        player.Velocity = Vector2.LerpPrecise(player.Velocity, Vector2.Zero, EaseLevel);
        IsControllable = true;
        IsDashing = false;
    }
    // debug returnables for helping see issues and stuf
    
    
    // main constructor
    public PlayerMovement() 
    {
        dashCool = new(0.55f, TimeStates.Down, false, false);
        dashDur = new(0.2f, TimeStates.Down, false, false);
        staminaRegen = new(1.5f, TimeStates.Down, true, false);
    }
    public void UpdateMovement(GameTime gt, Player player) 
    {
        if (!IsActive) return;
        Input.UpdateInputs();
    }
}
