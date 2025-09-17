using Microsoft.Xna.Framework;
using GameComponents.Logic;
using System;
namespace GameComponents.Helpers;
public static class Easing 
{
    public static float EaseInSine(float v) 
    {
        return (float)(1 - Math.Cos((v * Math.PI) / 2));
    }
    public static float EaseOutSine(float v) 
    {
        return (float)Math.Sin((v * Math.PI) / 2);
    }
    public static float EaseInOutSine(float v) 
    {
        return (float)-(Math.Cos(Math.PI * v) - 1) / 2;
    }
    public static float EaseInCubic(float v) 
    {
        return v * v * v;
    }
    public static float EaseOutCubic(float v) 
    {
        return 1 - (float)Math.Pow(1 - v, 3);
    }
    public static float EaseInOutCubic(float v) 
    {
        return v < 0.5f ? 4 * v * v * v : 1 - (float)Math.Pow(-2 * v + 2, 3) / 2;
    }
    public static float EaseInQuint(float v) 
    {
        return v * v * v * v * v;
    }
    public static float EaseOutQuint(float v) 
    {
        return 1 - (float)Math.Pow(1 - v, 5);
    }
    public static float EaseInOutQuint(float v) 
    {
        return v < 0.5f ? 16 * v * v * v * v * v : 1 - (float)Math.Pow(-2 * v + 2, 5) / 2;
    }
    public static float EaseIn
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