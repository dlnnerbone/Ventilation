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
    public static float EaseInCirc(float v) 
    {
        return 1 - (float)Math.Sqrt(1 - Math.Pow(v, 2));
    }
    public static float EaseOutCirc(float v) 
    {
        return 1 - (float)Math.Sqrt(1 - Math.Pow(v - 1, 2));
    }
    public static float EaseInOutCirc(float v) 
    {
        return v < 0.5 ? (1 - (float)Math.Sqrt(1 - Math.Pow(2 * v, 2))) / 2 :
            (float)(Math.Sqrt(1 - Math.Pow(-2 * v + 2, 2)) + 1) / 2;
    }
    public static float EaseInElastic(float v) 
    {
        float c4 = (float)(2 * Math.PI) / 3;

        return v == 0 ? 0 : v == 1 ? 1 : (float)-Math.Pow(2, 10 * v - 10) * (float)Math.Sin((v * 10 - 10.75) * c4);
    }
    public static float EaseOutElastic(float v) 
    {
        float c4 = (float)(2 * Math.PI) / 3;

        return v == 0 ? 0 : v == 1 ? 1 : (float)Math.Pow(2, -10 * v) * (float)Math.Sin((v * 10 - 0.75) * c4) + 1;
    }
    public static float EaseInOutElastic(float v) 
    {
        float c5 = (float)(2 * Math.PI) / 4.5f;

        return v == 0 ? 0 : v == 1 ? 1 : v < 0.5f ? (float)-Math.Pow(2, 10 * v - 10) * (float)Math.Sin((20 * v - 11.125f) * c5) / 2 :
            (float)(Math.Pow(2, -20 * v + 10) * (float)Math.Sin((20 * v - 11.125f) * c5)) / 2 + 1;
    }
    public static float EaseInQuad(float v) 
    {
        return v * v;
    }
    public static 
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