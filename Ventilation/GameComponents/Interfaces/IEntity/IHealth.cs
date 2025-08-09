using System;
namespace GameComponents.Interfaces;
public interface IHealthComponent 
{
    public float Health { get; set; }
    public float MinHealth { get; set; }
    public float MaxHealth { get; set; }
}