using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Entity;
using GameComponents;
using GameComponents.Logic;
using GameComponents.Managers;
namespace Main;
public sealed class PlayerCombat 
{
    public InputManager Input { get; private set; } = new();
    public Timer GeneralCooldown { get; private set; } = new(0.75f);
    public Timer BulletCooldown { get; private set; } = new(0.5f);
    public Bullet bullet { get; private set; }
    // main gimmik
    public void Initialize(Player P) 
    {
        bullet = new(Vector2.UnitX, (int)P.Center.X, (int)P.Center.Y);
    }
    public void LoadContent(GraphicsDevice device) 
    {
        bullet.LoadContent(device);
    }
    public void UpdateCombat(GameTime gt, Player P) 
    {
        Input.UpdateInputs();
        GeneralCooldown.TickTock(gt);
        BulletCooldown.TickTock(gt);
        bullet.ShootingTime(gt);
        if (Input.IsLeftClicked) bullet.SetActionState(Actions.Active); // 
        else if (Input.IsRightClicked) bullet.SetActionState(Actions.Interrupted);
        
    }
    public void Draw(SpriteBatch batch) 
    {
        bullet.Draw(batch);
    }
}