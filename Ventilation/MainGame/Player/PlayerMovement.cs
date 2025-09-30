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
    private Timer dashCool, dashDur, staminaCool;
    private InputManager input = new();
    private Vector2 velocity = Vector2.Zero;
    private Motions motionState = Motions.Idle;
    private float moveSpeed = 400f;
    private float dashForce = 2000f;
    private float speedMultiplier = 1f;
    private float stamina = 100f;
    private float maxStamina = 100f;
    // public properties
    public Motions MotionState { get => motionState; set => motionState = value; }
    public Vector2 MovementFontPosition { get; set; } = new Vector2(50, 150);
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value * speedMultiplier; }
    public float DashForce { get => dashForce; set => dashForce = value < moveSpeed ? moveSpeed + 100 : value; }
    public float Stamina { get => stamina; set => stamina = MathHelper.Clamp(value, 0f, maxStamina); }
    public float MaxStamina { get => maxStamina; set => maxStamina = Math.Abs(value); }
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = Math.Abs(value); }
    public bool IsControllable { get; set; } = true;
    public bool CanDash { get; set; } = true;
    public bool IsDashing { get; set; } = false;
    public PlayerMovement() 
    {
        dashCool = new Timer(0.55f);
        dashDur = new Timer(0.2f);
        staminaCool = new(1.65f, TimeStates.Down, true);
    }
    private void Idle() 
    {
        velocity = Vector2.LerpPrecise(velocity, Vector2.Zero, 0.4f);
        IsDashing = false;
        IsControllable = true;
    }
    private void Moving() 
    {
        velocity = Vector2.Zero;
        if (input.IsKeyDown(Keys.W)) velocity.Y = -MoveSpeed;
        else if (input.IsKeyDown(Keys.S)) velocity.Y = MoveSpeed;
        if (input.IsKeyDown(Keys.A)) velocity.X = -MoveSpeed;
        else if (input.IsKeyDown(Keys.D)) velocity.X = MoveSpeed;
    }
    private void Dashing(GameTime gt, Player player) 
    {
        StaminaIntake(3f * Easing.EaseOutSine(dashDur.NormalizedProgress));
        velocity = player.Direction * DashForce * (float)gt.ElapsedGameTime.TotalSeconds;
        if (staminaCool.TimeHitsFloor()) 
        {
            MotionState = Motions.Idle;
        }
    }
    private void StateManagement() 
    {
        if (IsControllable && input.WASD) MotionState = Motions.Moving;
        else if (!IsDashing) MotionState = Motions.Idle;
        if (!IsDashing && IsControllable && Stamina <= 0 && input.IsKeyPressed(Keys.LeftShift) && CanDash && !dashCool.TimeHitsFloor()) 
        {
            IsDashing = true;
            IsControllable = false;
            dashCool.Restart();
            dashDur.Restart();
            MotionState = Motions.Dashing;
        }
    }
    public void UpdateMovement(GameTime gt, Player player) 
    {
        velocity = player.Velocity;
        input.UpdateInputs();
        updateTimers(gt);
        StateManagement();
        switch (motionState) 
        {
            case Motions.Idle: Idle(); break;
            case Motions.Moving: Moving();  break;
            case Motions.Dashing: Dashing(gt, player); break;
        }
        player.Velocity = velocity;
    }
    public void StaminaIntake(float intakeAmount) 
    {
        Stamina -= intakeAmount;
    }
    private void updateTimers(GameTime gt) 
    {
        dashCool.TickTock(gt);
        dashDur.TickTock(gt);
        staminaCool.TickTock(gt);
    }
    public void DrawMovementStats(SpriteBatch batch, SpriteFont font) 
    {
        batch.DrawString(font, $"{Stamina}", MovementFontPosition, Color.CornflowerBlue);
    }
}
