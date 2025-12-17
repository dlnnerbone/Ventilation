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
    
    public Player() : base(50, 250, 16 * 4, 16 * 4, 100, 0, 100) {}
    
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
            {1, 0, 0, 0, 0, 1}
        }, true);
        
        MapLogic.IsLogicActive = true;
        
        MapVisual.SetSourceGrid(grid);
        MapLogic.ToggleCollision(new HashSet<int> {0}, false);
        
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
            if (!Intersects(c.Bounds)) return;
            
            var rightOverlap = Right - c.Bounds.Left;
            var leftOverlap = Math.Abs(Left - c.Bounds.Right);
            var topOverlap = Math.Abs(Top - c.Bounds.Bottom);
            var bottomOverlap = Bottom - c.Bounds.Top;
            
            bool isTouchingRight = rightOverlap < leftOverlap;
            bool isTouchingLeft = leftOverlap < rightOverlap;
            bool isTouchingTop = topOverlap < bottomOverlap;
            bool isTouchingBottom = bottomOverlap < topOverlap;
            
            if (isTouchingRight && Velocity_X > 0) 
            {
                X -= (int)rightOverlap;
                Velocity_X = 0;
            }
            else if (isTouchingLeft && Velocity_X < 0) 
            {
                X += (int)leftOverlap;
                Velocity_X = 0;
            }
            
            if (isTouchingTop && Velocity_Y < 0) Y += (int)topOverlap;
            
            Diagnostics.Write($"right: {rightOverlap}, {leftOverlap}");
        });
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        MapVisual.Draw(spriteBatch, tileSet, Color.White);
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        
    }
}