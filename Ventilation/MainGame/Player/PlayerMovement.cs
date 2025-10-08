using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Logic;
using GameComponents.Managers;
using System;
using Microsoft.Xna.Framework.Graphics;
namespace Main;
public class PlayerMovement 
{
    private Timer dashCool, dashDur, staminaRegen;
    private Motions motionState = Motions.Idle;
    private float moveSpd = 100f, maxSpd = 1000f, speedMulti = 1, dashForce = 2000f, stamina = 100f, maxStamina = 100f, easeLvl = 0.2f;
    
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
    private void Moving(Player player) 
    {
        IsDashing = false;
        IsControllable = true;
        player.Velocity = Vector2.Clamp(player.Velocity, new Vector2(-MaxSpeed, -MaxSpeed), new Vector2(MaxSpeed, MaxSpeed));
        // controls for moving up
        if (Input.IsKeyDown(Keys.W)) player.Velocity_Y -= MoveSpeed;
        else if (Input.IsKeyDown(Keys.S)) player.Velocity_Y += MoveSpeed;
        else player.Velocity_Y = MathHelper.LerpPrecise(player.Velocity_Y, 0, EaseLevel);

        if (Input.IsKeyDown(Keys.A)) player.Velocity_X -= MoveSpeed;
        else if (Input.IsKeyDown(Keys.D)) player.Velocity_X += MoveSpeed;
        else player.Velocity_X = MathHelper.LerpPrecise(player.Velocity_X, 0, EaseLevel);
    }
    private void Dashing(Player player) 
    {
        player.Velocity = player.Direction * DashForce;
        if (dashDur.TimeHitsFloor()) 
        {
            SwitchStates(Motions.Idle);
        }
    }
    // the main updater for movement
    private void movementManager() 
    {
        Input.UpdateInputs();

        var canDash = CanDash && !IsDashing && Stamina > 0 && IsControllable && dashCool.TimeHitsFloor();
        
        if (Input.WASD && IsControllable) SwitchStates(Motions.Moving);
        else if (!IsDashing) SwitchStates(Motions.Idle);
        if (canDash && Input.IsKeyPressed(Keys.LeftShift)) 
        {
            IsDashing = true;
            IsControllable = false;
            dashCool.Restart();
            dashDur.Restart();
            SwitchStates(Motions.Dashing);
            Diagnostics.Write("Dashed");
        }
    }
    
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
        timerManager(gt);
        movementManager();
        switch(motionState) 
        {
            case Motions.Idle: Idle(player); break;
            case Motions.Moving: Moving(player); break;
            case Motions.Dashing: Dashing(player); break;
        }
    }
    private void timerManager(GameTime gt) 
    {
        dashCool.TickTock(gt);
        dashDur.TickTock(gt);
        staminaRegen.TickTock(gt);
    }
    public void DisplayPlayerMovementStats(SpriteBatch batch) 
    {
        
    }
}
