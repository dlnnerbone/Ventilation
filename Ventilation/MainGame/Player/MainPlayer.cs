using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Logic;
using System.Collections.Generic;
using GameComponents.Helpers;
using GameComponents;
using System;
namespace Main;

public sealed class Player : Entity
{
    public CharacterMovementModule Movement { get; set; }
    public PlayerCombatModule combatModule { get; private set; }
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    
    TileMapVisuals MapVisual;
    TileGrid grid;
    Texture2D tileSet;
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.Purple); 
        PlayerSprite.LayerDepth = 1;
        
        tileSet = content.Load<Texture2D>("Game/Assets/TileSets/BasicTileSet");
        grid = new(4, 4, tileSet);
        
        MapVisual = new(Vector2.Zero, LayoutDirection.Horizontal, 128, new byte[,] 
        {
            {1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 1}
        });
        
        MapVisual.SetSourceGrid(grid);
        
        combatModule = new(content);
        
    }
    
    protected override void MoveAndSlide(GameTime gt) => Position += Velocity * (float)gt.ElapsedGameTime.TotalSeconds;
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        
        MoveAndSlide(gt);
        
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        
        var tile = MapVisual.GetNeighbouringTopTile(1, 3);
        MapVisual.GetNeighbouringTopTile(1, 3).Bounds = new(tile.Bounds.X + 1, tile.Bounds.Y - 1, tile.Bounds.Width, tile.Bounds.Height);
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        MapVisual.Draw(spriteBatch, tileSet, Color.White);
    }
}