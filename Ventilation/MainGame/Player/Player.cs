using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Rendering;
namespace Main;
public class Player : Entity 
{
    private PlayerMovement playerMotion;
    public PlayerStats PlayerStats { get; private set; }

    // player related variables
    public Motions SetMotion(Motions motion) => playerMotion.SetMotion(motion);
    public Motions MotionState => playerMotion.MotionState;
    public float MoveSpeed { get { return playerMotion.MoveSpeed; } set { playerMotion.MoveSpeed = value; } }
    public float LerpSpeed => playerMotion.LerpSpeed;
    public float MaxSpeed { get { return playerMotion.MaxSpeed; } set { playerMotion.MaxSpeed = value; } }
    public bool IsDashing { get { return playerMotion.IsDashing; } set { playerMotion.IsDashing = value; } }
    public bool IsControllable { get; set; } = true;
    public int Stamina { get { return playerMotion.Stamina; } set { playerMotion.Stamina = value; } }
    public int MaxStamina { get { return playerMotion.MaxStamina; } set { playerMotion.MaxStamina = value; } }
    public float DashForce { get { return playerMotion.DashForce; } set { playerMotion.DashForce = value; } }
    
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        playerMotion = new();
    }
    public void LoadContent(ContentManager manager) 
    {
        PlayerSprite = new(manager.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"), Color.White);
        PlayerStats = new(manager);
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
        PlayerSprite.Draw(batch, Bounds, new Rectangle(64, 0, 64, 64));
    }
    public void DrawStats(SpriteBatch batch) 
    {
        PlayerStats.DrawStats(batch, this);
    }
    
}