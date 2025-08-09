using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class HealthComponent : IHealthComponent 
{
    protected float health;
    protected float minHealth;
    private float maxHealth;
    // private fields
    public virtual float Health { get { return health; } set { health = MathHelper.Clamp(value, minHealth, maxHealth); } }
    public virtual float MinHealth { get { return minHealth; } set { minHealth = value > maxHealth ? maxHealth - 1 : value; } }
    public virtual float MaxHealth { get { return maxHealth; } set { maxHealth = value < minHealth ? minHealth + 1 : value; } }
    public HealthComponent(float HP, float minHP, float maxHP) 
    {
        health = HP;
        minHealth = minHP;
        maxHealth = maxHP;
    }
    public void Destroy() => Health = minHealth;
    
}