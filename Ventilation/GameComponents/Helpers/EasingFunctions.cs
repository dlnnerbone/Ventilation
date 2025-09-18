using System;
namespace GameComponents.Helpers;
public static class Easing 
{
    private const float c4 = (float)(2 * Math.PI) / 3;
    private const float c5 = (float)(2 * Math.PI) / 4.5f;
    private const float c1 = 1.70158f;
    private const float n1 = 7.5625f;
    private const float d1 = 2.75f;
    
    
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
        return v < 0.5f ? (1 - (float)Math.Sqrt(1 - Math.Pow(2 * v, 2))) / 2 :
            (float)(Math.Sqrt(1 - Math.Pow(-2 * v + 2, 2)) + 1) / 2;
    }
    public static float EaseInElastic(float v) 
    {
        return v == 0 ? 0 : v == 1 ? 1 : (float)-Math.Pow(2, 10 * v - 10) * (float)Math.Sin((v * 10 - 10.75) * c4);
    }
    public static float EaseOutElastic(float v) 
    {
        return v == 0 ? 0 : v == 1 ? 1 : (float)Math.Pow(2, -10 * v) * (float)Math.Sin((v * 10 - 0.75) * c4) + 1;
    }
    public static float EaseInOutElastic(float v) 
    {
        return v == 0 ? 0 : v == 1 ? 1 : v < 0.5f ? (float)-Math.Pow(2, 10 * v - 10) * (float)Math.Sin((20 * v - 11.125f) * c5) / 2 :
            (float)(Math.Pow(2, -20 * v + 10) * (float)Math.Sin((20 * v - 11.125f) * c5)) / 2 + 1;
    }
    public static float EaseInQuad(float v) 
    {
        return v * v;
    }
    public static float EaseOutQuad(float v) 
    {
        return 1 - (1 - v) * (1 - v);
    }
    public static float EaseInOutQuad(float v) 
    {
        return v < 0.5f ? 2 * v * v : 1 - (float)Math.Pow(-2 * v + 2, 2) / 2;
    }
    public static float EaseInQuart(float v) 
    {
        return v * v * v * v;
    }
    public static float EaseOutQuart(float v) 
    {
        return 1 - (float)Math.Pow(1 - v, 4);
    }
    public static float EaseInOutQuart(float v) 
    {
        return v < 0.5f ? 8 * v * v * v * v : 1 - (float)Math.Pow(-2 * v + 2, 4) / 2;
    }
    public static float EaseInExpo(float v) 
    {
        return v == 0 ? 0 : (float)Math.Pow(2, 10 * v - 10);
    }
    public static float EaseOutExpo(float v) 
    {
        return v == 1 ? 1 : 1 - (float)Math.Pow(2, -10 * v);
    }
    public static float EaseInOutExpo(float v) 
    {
        return v == 0 ? 0 : v == 1 ? 1 : v < 0.5 ? (float)Math.Pow(2, 20 * v - 10) / 2 :
            (2 - (float)Math.Pow(2, -20 * v + 10)) / 2;
    }
    public static float EaseInBack(float v) 
    {
        const float c3 = c1 + 1;
        return c3 * v * v * v - c1 * v * v;
    }
    public static float EaseOutBack(float v) 
    {
        const float c3 = c1 + 1;
        return 1 + c3 * (float)Math.Pow(v - 1, 3) + c1 * (float)Math.Pow(v - 1, 2);
    }
    public static float EaseInOutBack(float v) 
    {
        const float c2 = c1 * 1.525f;
        return v < 0.5f ? ((float)Math.Pow(2 * v, 2) * ((c2 + 1) * 2 * v - c2)) / 2
        : ((float)Math.Pow(2 * v - 2, 2) * ((c2 + 1) * (v * 2 - 2) + c2) + 2) / 2;
    }
    public static float EaseInBounce(float v) 
    {
        return 1 - EaseOutBounce(1 - v);
    }
    public static float EaseInOutBounce(float v) 
    {
        return v < 0.5f ? (1 - EaseOutBounce(1 - 2 * v)) / 2
        : (1 + EaseOutBounce(2 * v - 1)) / 2;
    }
    public static float EaseOutBounce(float v) 
    {
        return v < 1 / d1 ? n1 * v * v
        : v < 2 / d1 ? n1 * (v - 1.5f / d1) * v + 0.75f
        : v < 2.5f / d1 ? n1 * (v - 2.25f / d1) * v + 0.9375f
        : n1 * (v - 2.625f / d1) * v + 0.984375f; 
    }
}