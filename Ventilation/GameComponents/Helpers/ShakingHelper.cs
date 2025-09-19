using Microsoft.Xna.Framework;
using System;
namespace GameComponents.Helpers;
public static class ShakeHelper 
{
    private static Random r = new();
    private const float TWO_PI = (float)Math.PI * 2;
    
    public static Vector2 InSineShake(float intensity, float progress) 
    {
        float t = Easing.EaseInSine(progress);
        float currentIntensity = t * intensity;

        float Angle = (float)(r.NextDouble() * TWO_PI);
        return new Vector2((float)Math.Cos(Angle) * currentIntensity, (float)Math.Sin(Angle) * currentIntensity);
    }
    public static Vector2 OutSinShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutSine(progress);
        float curInt = t * intensity;
        float Angle = (float)r.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    }
    public static Vector2 InOutSineShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutSine(progress);
        float curInt = t * intensity;
        float Angle = (float)r.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    }
    public static Vector2 InCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseInCubic(progress);
        float curInt = t * intensity;
        float Angle = (float)r.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    }
    public static Vector2 OutCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutCubic(progress);
        float curInt = t * intensity;
        float Angle = (float)r.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    }
    public static Vector2 InOutCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutCubic(progress);
        float curInt = t * intensity;
        float Angle = (float)r.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    }
}