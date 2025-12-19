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
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1},
            {1,2,2,2,2,2,2,2,2,2,2,2,3,1}
        });
        
        MapLogic = new(LayoutDirection.Horizontal, Vector2.Zero, 128, new byte[,] 
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1},
            {1,2,2,2,2,2,2,2,2,2,2,2,3,1}
        }, false);
        
        MapLogic.IsLogicActive = true;
        
        MapVisual.SetSourceGrid(grid);
        MapLogic.ToggleCollision(new HashSet<int> {1, 3}, true);
        
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
            
            Vector2 colliderCenter = new Vector2(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2);
            Vector2 vectorDistance = Center - colliderCenter;
            
            float minDistanceX = Width / 2 + c.Bounds.Width / 2;
            float minDistanceY = Height / 2 + c.Bounds.Height / 2;
            
            float overlapX = Center.X < colliderCenter.X ? vectorDistance.X + minDistanceX : Math.Abs(vectorDistance.X - minDistanceX);
            float overlapY = Center.Y < colliderCenter.Y ? vectorDistance.Y + minDistanceY : Math.Abs(vectorDistance.Y - minDistanceY);
            
            bool isTouchingHorizontally = overlapX < overlapY;
            
            if (isTouchingHorizontally) 
            {
                if (Center.X < colliderCenter.X) X -= (int)overlapX;
                else X += (int)overlapX;
            }
            else 
            {
                if (Center.Y < colliderCenter.Y) Y -= (int)overlapY;
                else Y += (int)overlapY;
            }
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