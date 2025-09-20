using Microsoft.Xna.Framework;
using System;
using System.Threading;
namespace GameComponents.Helpers;
public static class ShakeHelper 
{
    private static ThreadLocal<Random> r = new(() => new Random());
    private const float TWO_PI = (float)Math.PI * 2;
    
    private static Vector2 Returnable(float t, float intensity) 
    {
        float curInt = t * intensity;
        float Angle = (float)r.Value.NextDouble() * TWO_PI;
        return new Vector2((float)Math.Cos(Angle) * curInt, (float)Math.Sin(Angle) * curInt);
    } 
    
    public static Vector2 InSineShake(float intensity, float progress) 
    {
        float t = Easing.EaseInSine(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutSinShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutSine(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutSineShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutSine(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseInCubic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutCubic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutCubicShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutCubic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InQuintShake(float intensity, float progress) 
    {
        float t = Easing.EaseInQuint(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutQuintShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutQuint(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutQuintShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutQuint(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InCircShake(float intensity, float progress) 
    {
        float t = Easing.EaseInCirc(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutCircShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutCirc(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutCircShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutCirc(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InElasticShake(float intensity, float progress) 
    {
        float t = Easing.EaseInElastic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutElasticShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutElastic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutElasticShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutElastic(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InQuadShake(float intensity, float progress) 
    {
        float t = Easing.EaseInQuad(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutQuadShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutQuad(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutQuadShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutQuad(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InQuartShake(float intensity, float progress) 
    {
        float t = Easing.EaseInQuart(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutQuartShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutQuart(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutQuartShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutQuart(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InExpoShake(float intensity, float progress) 
    {
        float t = Easing.EaseInExpo(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutExpoShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutExpo(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutExpoShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutExpo(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InBackShake(float intensity, float progress) 
    {
        float t = Easing.EaseInBack(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutBackShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutBack(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutBackShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutBack(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InBounceShake(float intensity, float progress) 
    {
        float t = Easing.EaseInBounce(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 OutBounceShake(float intensity, float progress) 
    {
        float t = Easing.EaseOutBounce(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 InOutBounceShake(float intensity, float progress) 
    {
        float t = Easing.EaseInOutBounce(progress);
        return Returnable(t, intensity);
    }
    public static Vector2 LinearShake(float intensity, float progress) 
    {
        return Returnable(progress, intensity);
    }
}