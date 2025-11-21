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
    public readonly ProjectilePool<WebClump> ClumpPool;
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
        Clumps = [new(), new(), new(), new(), new()];
        
        for(int i = 0; i < Clumps.Count; i++) 
        {
            Clumps[i].LoadContent(content);
        }
    }
    
}