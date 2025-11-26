using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameComponents.Managers;
using GameComponents.Logic;
using GameComponents;
using GameComponents.Entity;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Main;
public sealed class PlayerCombatModule 
{
    // private fields
    private int maxProjectiles = 5;
    private bool _isStunned = false;
    private Vector2 mousePos => MouseManager.WorldMousePosition;
    // properties
    public readonly Timer GeneralCooldown;
    public int MaxProjectiles { get => maxProjectiles; set => maxProjectiles = Math.Abs(value); }
    public readonly ObjectPool<WebClump> ClumpPool;
    public readonly List<WebClump> Clumps;
    public readonly InputManager Input = new();
    // methods
    public void Stun() => _isStunned = true;
    
    public void PushToPool(int index) 
    {
        ClumpPool.Push(Clumps[index]);
        Clumps.RemoveAt(index);
    }
    
    public PlayerCombatModule(ContentManager content) 
    {
        GeneralCooldown = new(0.4f);
        ClumpPool = new(() => new WebClump());
        Clumps = new();
        
        for (int i = 0; i <= maxProjectiles; i++) 
        {
            var clump = new WebClump();
            clump.LoadContent(content);
            Clumps.Add(clump);
        }
        
        for(int i = Clumps.Count - 1; i >= 0; i--) PushToPool(i);
    }
    
    public void UpdateCombat(GameTime gt, Entity owner) 
    {
        GeneralCooldown.TickTock(gt);
        Input.UpdateInputs();
        
        _generalizedSelectionManager(gt, owner);
        _singularSelectionManager(owner);
    }
    // the main function methods.
    private void _generalizedSelectionManager(GameTime gt, Entity owner) 
    {
        // for-loop to update every Projectile.
        
        for(int i = Clumps.Count - 1; i >= 0; i--) 
        {
            // to update every projectile.
            Clumps[i].ShootingTime(gt);
            // check if the unit-scale of the life-span of each projectile hits zero to be pushed into the Inactive List.
            if (Clumps[i].NormalizedLifeSpan <= 0) 
            {
                Clumps[i].SetDestination(owner.Center);
                Clumps[i].OverrideFlags(Actions.Cooldown);
            }
            // pushes them in.
            if (Clumps[i].DistanceFrom(owner.Center) <= 50f && Clumps[i].InCooldown) 
            {
                PushToPool(i);
            }
        }
    }
    
    private void _singularSelectionManager(Entity owner) 
    {
        bool leftClick = MouseManager.IsLeftClicked;
        bool countCheck = Clumps.Count - 1 >= 0;
        
        if (Clumps.Count > maxProjectiles) PushToPool(0);
        
        if ((leftClick && Clumps.Count - 1 < 0) || (leftClick && (Clumps[^1].IsCurrentlyActive || Clumps[^1].InCooldown))) 
        {
            Clumps.Add(ClumpPool.Request());
        }
        
        if (MouseManager.IsLeftHeld && !_isStunned) 
        {
            Clumps[^1].AimAt(mousePos);
            Clumps[^1].SetDestination(owner.Center);
            Clumps[^1].OverrideFlags(Actions.Ready);
        }
        else if (countCheck && !Clumps[^1].InCooldown) Clumps[^1].OverrideFlags(Actions.Active);
        
    }
    
    public void DrawCombat(SpriteBatch batch) 
    {
        foreach(var web in Clumps) web.DrawProjectile(batch);
    }
}