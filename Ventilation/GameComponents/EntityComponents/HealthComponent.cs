using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class HealthComponent : IHealthComponent 
{
    /// <summary>
    /// a class for adding health parameters.
    /// </summary>
    private float health = 0;
    private float minHealth = 0;
    private float maxHealth = 0;
    // private fields
    public float Health { get { return health; } set { health = MathHelper.Clamp(value, minHealth, maxHealth); } }
    public float MinHealth { get { return minHealth; } set { minHealth = value > maxHealth ? maxHealth - 1 : value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value < minHealth ? minHealth + 1 : value; } }
    public float NormalizedHealth => (health - minHealth) / (maxHealth - minHealth);
    public HealthComponent(float HP, float minHP, float maxHP = 100) 
    {
        Health = HP != 0 ? HP : maxHP;
        MinHealth = minHP;
        MaxHealth = maxHP;
    }
    public bool IsFullHealth() => health >= maxHealth;
    public bool IsWithinCriticalThreshold(float value) => health <= value;
    public HealthComponent Destroy() => new HealthComponent(0, this.MinHealth, this.MaxHealth);
}