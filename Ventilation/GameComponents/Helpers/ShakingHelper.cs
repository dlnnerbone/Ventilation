using Microsoft.Xna.Framework;
using System;
using GameComponents.Logic;
namespace GameComponents.Helpers;
public static class ShakeHelper 
{
    // this is a very minimal and specific side of the class that helps with General use of shaking objects and cameras.
    private static Random random = new();
    // Linear Shaking
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
    public static Vector2 LinearShake(float intensity, Timer timer) 
    {
        if (timer.TimeSpan >= timer.Duration) return Vector2.Zero;
        // dampen values
        var t = 1.0f - (timer.TimeSpan / timer.Duration);
        var currentIntensity = intensity * t;
        // random direction
        float Angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(Angle) * currentIntensity, (float)Math.Sin(Angle) * currentIntensity);
    }
    // Expo shaking
    public static Vector2 ExponentialShake(float intensity, float duration, float elapsedTime) // I made this class for making shaky effects
    {
        if (elapsedTime >= duration) return Vector2.Zero;
        // dampen
        var t = (float)Math.Exp(-elapsedTime * intensity / duration);
        var currentIntensity = intensity * t;
        // returnable
        var angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(angle) * currentIntensity, (float)Math.Sin(angle) * currentIntensity);
    }
    public static Vector2 ExponentialShake(float intensity, Timer time) 
    {
        if (time.TimeSpan >= time.Duration) return Vector2.Zero;
        // dampen overtime
        var t = (float)Math.Exp(-time.TimeSpan * intensity / time.Duration);
        var currentIntensity = intensity * t;
        // angle
        float Angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(Angle) * currentIntensity, (float)Math.Sin(Angle) * currentIntensity);
    }
}