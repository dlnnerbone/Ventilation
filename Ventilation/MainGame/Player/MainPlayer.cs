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
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) {}
    
    TileMap Map;
    TileGrid grid;
    Texture2D texture;
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.Purple);
        PlayerSprite.LayerDepth = 1;
        
        combatModule = new(content);
        
        texture = content.Load<Texture2D>("Game/Assets/TileSets/BasicTileSet");
        grid = new(4, 4, texture);
        
        Map = new(new byte[,] 
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 1}
        } 
        , new byte[,] 
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 1}
        } 
        , Vector2.Zero, 128, 128, LayoutDirection.Horizontal);
        
        Map.SetSourceGrid(grid);
        
        Map.ToggleCollidersFromLayout(new HashSet<byte> { 1, 3 }, true);
        
    }
    
    protected override void MoveAndSlide(GameTime gt) => Position += Velocity * (float)gt.ElapsedGameTime.TotalSeconds;
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        
        MoveAndSlide(gt);
        
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        
        Map.Update((int i, ref Collider collider) => 
        {
            if (!collider.Bounds.Intersects(Bounds)) return;
            
            var distanceFromLeft = Left - collider.Bounds.Left;
            var distanceFromRight = Right - collider.Bounds.Right;
            var distanceFromTop = Top - collider.Bounds.Top;
            var distanceFromBottom = Bottom - collider.Bounds.Bottom;
            
            if (distanceFromLeft <= 128 && distanceFromLeft >= 64) X = collider.Bounds.Right;
            if (distanceFromRight <= 128 && distanceFromRight <= 64) X = collider.Bounds.Left - Width;
            
            Diagnostics.Write($"{i},{distanceFromLeft}");
        });
        
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        Map.Draw(spriteBatch, texture);
    }
}