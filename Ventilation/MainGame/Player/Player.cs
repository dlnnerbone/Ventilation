using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
namespace Main;
public class Player : Entity 
{
    private PlayerMovement playerMotion;

    // player related variables
    public float MoveSpeed { get { return playerMotion.MoveSpeed; } set { playerMotion.MoveSpeed = value; } }
    public float LerpSpeed => playerMotion.LerpSpeed;
    public float MaxSpeed { get { return playerMotion.MaxSpeed; } set { playerMotion.MaxSpeed = value; } }
    public bool IsDashing { get { return playerMotion.IsDashing; } set { playerMotion.IsDashing = value; } }
    public bool IsControllable { get; set; } = true;
    public int Stamina { get { return playerMotion.Stamina; } set { Stamina = value; } }
    public int MaxStamina { get { return playerMotion.MaxStamina; } set { playerMotion.MaxStamina = value; } }
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        playerMotion = new();
    }
    public void LoadContent(Game game) 
    {
        PlayerSprite = new(new(game.GraphicsDevice, 1, 1), Color.Red);
        PlayerSprite.SetToData();
    }
    public void UpdateLogic(GameTime gt) 
    {
        if (!IsAlive) return;
        MoveAndSlide(gt);
        playerMotion.HandlePlayerMovement(gt, this);
    }
    public void Draw(SpriteBatch batch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(batch, Bounds);
    }
    
}