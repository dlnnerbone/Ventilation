using Microsoft.Xna.Framework;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents;
using System;
namespace Main;
public sealed class Player : Entity
{
    public CharacterMovementModule Movement { get; private set; }
    public PlayerCombatModule combatModule { get; private set; }
    public readonly ParticleManager particleManager;
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    private Texture2D texture;
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) 
    {
        particleManager = new(100);
        particleManager.Initialize(() => new Particle());
    }
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.White);
        
        combatModule = new(content);
        
        texture = new(device, 1, 1);
        texture.SetData(new Color[] { Color.Red });
    }
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        MoveAndSlide(gt);
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        int count = 0;
        
        particleManager.UpdateParticles((ref Particle p) => 
        {
            count++;
            p.LayerDepth = 1f;
            
            p.Radians += (float)gt.ElapsedGameTime.TotalSeconds * (float)Math.Sqrt(count);
            
            p.Position = Center - new Vector2(5, 5) + p.Direction * new Vector2(300f / (float)Math.Sqrt(count), 100f / (float)Math.Sqrt(count));
        });
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        particleManager.Draw(spriteBatch, texture, new Rectangle(0, 0, 10, 10), new Vector2(5, 5)); // 
    }
}