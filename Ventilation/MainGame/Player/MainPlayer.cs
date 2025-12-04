using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Helpers;
using Microsoft.Xna.Framework.Input;
using GameComponents.Logic;
namespace Main;

public sealed class Player : Entity
{
    public CharacterMovementModule Movement { get; set; }
    public PlayerCombatModule combatModule { get; private set; }
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    
    private TileMap map;
    private TileGrid grid;
    private Texture2D texture;
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.White);
        
        combatModule = new(content);
        
        texture = content.Load<Texture2D>("Game/Assets/TileSets/BasicTileSet");
        grid = new TileGrid(4, 4, texture);
        map = new TileMap(Vector2.Zero, grid, 64 * 2, 64 * 2, new int[,] 
        {
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 1, 1, 1}
        });
    }
    
    protected override void MoveAndSlide(GameTime gt) => Position += Velocity * (float)gt.ElapsedGameTime.TotalSeconds;
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        
        MoveAndSlide(gt);
        
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        map.Draw(spriteBatch, texture);
    }
}