using Microsoft.Xna.Framework;
using GameComponents.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Entity;
using GameComponents.Rendering;
using GameComponents.Helpers;
using System;
using System.Text.Json.Serialization;
namespace Main;
public sealed class Player : Entity
{
    public CharacterMovementModule Movement { get; set; }
    public PlayerCombatModule combatModule { get; private set; }
    
    public Sprite PlayerSprite { get; private set; }
    public bool IsAlive { get; set; } = true;
    
    private Texture2D pTexture;
    private ParticleManager pM;
    
    public Player() : base(50, 250, 32 * 4, 32 * 4, 100, 0, 100) 
    {
        pM = new ParticleManager(100);
        pM.Initialize(() => new Particle 
        {
            LayerDepth = 1f,
            LifeTime = 1f,
            Age = 0f,
            Position = Vector2.Zero
        });
        
    }
    
    public void LoadContent(GraphicsDevice device, ContentManager content) 
    {
        Movement = new CharacterMovementModule(content);
        PlayerSprite = new Sprite(device, 1, 1);
        PlayerSprite.SetData(Color.White);
        PlayerSprite.LayerDepth = 0.1f;
        
        combatModule = new(content);
        
        pTexture = new(device, 1, 1);
        pTexture.SetData(new Color[] { Color.Red });
    }
    
    public void UpdatePlayer(GameTime gt) 
    {
        if (!IsAlive) return;
        MoveAndSlide(gt);
        Movement.UpdateMovement(gt, this);
        combatModule.UpdateCombat(gt, this);
        
        var count = 0;
        pM.UpdateParticles((ref Particle p, int i) => 
        {
            count++;
            var sqrtCount = (float)Math.Sqrt(count);
            if (i < 51) 
            {
                p.Radians += (float)gt.ElapsedGameTime.TotalSeconds * 2 * sqrtCount * 0.3f;
                p.Radians %= (float)Math.PI * 2;
                
                p.Position = Center - new Vector2(5, 5) + p.Direction * 100f / sqrtCount * 2f;
            }
            else 
            {
                p.Radians -= (float)gt.ElapsedGameTime.TotalSeconds * sqrtCount * 0.85f;
                p.Radians %= (float)Math.PI * 2;
                Diagnostics.Write($"{p.Radians}");
                p.Position = Center - new Vector2(5, 5) + p.Direction * 1000f / sqrtCount;
            }
        });
    }
    
    public void DrawPlayer(SpriteBatch spriteBatch) 
    {
        if (!IsAlive) return;
        PlayerSprite.Draw(spriteBatch, Bounds);
        combatModule.DrawCombat(spriteBatch);
        pM.Draw(spriteBatch, pTexture, new Rectangle(0, 0, 10, 10), new Vector2(5, 5));
    }
}