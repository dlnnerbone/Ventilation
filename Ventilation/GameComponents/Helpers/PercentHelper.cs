namespace GameComponents.Helpers;

public static class PercentageHelper
{
    public static float GetPercentage(float value, float totalAmount)
    {
        return value / totalAmount * 100;
    }
    public static float GetPercentage(int value, int totalAmount)
    {
        return value / totalAmount * 100;
    }
    public static float GetProportion(float value, float totalAmount)
    {
        return value / totalAmount;
    }
    public static float GetProportion(int value, float totalAmount)
    {
        return value / totalAmount;
    }
}