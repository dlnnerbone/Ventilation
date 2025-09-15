using Microsoft.Xna.Framework;
using System;
namespace GameComponents.helpers;
public static class InterpolationHelper 
{
    // this is a very minimal and specific static class that helps with General use of shaking objects and cameras.
    private static Random random = new();
    public static Vector2 LinearShake(float intensity, float duration, float elapsedTime) 
    {
        if (elapsedTime >= duration) return Vector2.Zero;
        // dampen values over time.
        var t = 1.0f - (elapsedTime / duration);
        var currentIntensity = intensity * t;
        // random direction
        float angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(angle) * currentIntensity, (float)Math.Sin(angle) * currentIntensity);
    }
    public static Vector2 ExponentialShake(float intensity, float duration, float elapsedTime) // I made this class for making shaky effects
    {
        if (elapsedTime >= duration) return Vector2.Zero;
        // dampen
        var t = (float)Math.Exp(-elapsedTime * 5f / duration);
        var currentIntensity = intensity * t;
        // returnable
        var angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(angle) * currentIntensity, (float)Math.Sin(angle) * currentIntensity);
    }
}