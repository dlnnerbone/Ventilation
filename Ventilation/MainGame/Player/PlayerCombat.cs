using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameComponents;
using GameComponents.Entity;
using GameComponents.Logic;
namespace Main;
public class CombatModule<T> where T : Projectile
{
    public readonly Timer GeneralCooldown;
    public readonly List<T> Projectiles;
    public Entity Target { get; set; }
    
    public CombatModule(Timer timer, int count) 
    {
        GeneralCooldown = timer;
        Projectiles = new List<T>(count);
    }  
}