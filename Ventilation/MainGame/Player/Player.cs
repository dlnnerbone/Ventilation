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
    public Sprite sprite;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) {}
    public void LoadContent(GraphicsDevice device) 
    {
        sprite = new(new(device, 1, 1), Color.White);
        sprite.SetToData();
    }
    public void UpdateLogic(GameTime gt) {}
    public void Draw(SpriteBatch batch) 
    {
        sprite.Draw(batch, Bounds);
    }
    
}