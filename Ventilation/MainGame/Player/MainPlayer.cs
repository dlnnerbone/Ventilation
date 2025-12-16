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
    TileMapLogic MapLogic;
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
        
        MapLogic = new(LayoutDirection.Horizontal, Vector2.Zero, 128, new byte[,] 
        {
            {1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 1, 0}
        }, true);
        
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
        
        MapLogic.Update((int i, ref Collider c) => 
        {
            Diagnostics.Write($"{MapLogic.GetNeighbouringLeftCollider(i).Bounds.X}");
        });
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        MapVisual.Draw(spriteBatch, tileSet, Color.White);
    }
}