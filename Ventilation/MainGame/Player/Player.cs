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
    public float LerpSpeed { get { return playerMotion.LerpSpeed; } private set { playerMotion.LerpSpeed = value; } }
    public float MaxSpeed { get { return playerMotion.MaxSpeed; } set { playerMotion.MaxSpeed = value; } }
    public bool IsDashing { get { return playerMotion.IsDashing; } set { playerMotion.IsDashing = value; } }
    public bool IsControllable { get; set; } = true;
    public int Stamina { get { return playerMotion.Stamina; } set { playerMotion.Stamina = value; } }
    public int MaxStamina { get { return playerMotion.MaxStamina; } set { playerMotion.MaxStamina = value; } }
    public float DashForce { get { return playerMotion.DashForce; } set { playerMotion.DashForce = value; } }
    public float SpeedMultiplier { get { return playerMotion.SpeedMultiplier; } set { playerMotion.SpeedMultiplier = value; } }
    public bool IsAlive { get; set; } = true;
    public Animation PlayerAnimation { get; private set; }
    public Player(int x, int y, int width, int height, float HP) : base(x, y, width, height, HP) 
    {
        playerMotion = new();
        SpeedMultiplier = 1f;
    }
    public void LoadContent(ContentManager manager) 
    {
        PlayerAnimation = new(new TextureAtlas(manager.Load<Texture2D>("PlayerAssets/CreatureSpriteIdle"), 4, 4), 0, 15);
        PlayerAnimation.FPS = 10;
        PlayerStats = new(manager);
    }
    public void UpdateLogic(GameTime gt) 
    {
        if (!IsAlive) return;
        PlayerAnimation.Roll(gt);
        MoveAndSlide(gt);
        playerMotion.HandlePlayerMovement(gt, this);
    }
    public void Draw(SpriteBatch batch) 
    {
        if (!IsAlive) return;
        PlayerAnimation.Scroll(batch, Bounds, Color.White);
    }
    public void DrawStats(SpriteBatch batch) 
    {
        PlayerStats.DrawStats(batch, this);
    }
    
}