namespace GameComponents.Helpers;

public static class MathHelper2
{
    /// <summary>
    /// a method to return a percentage of the given portions.
    /// </summary>
    /// <param name="portion">the current portion, e.g, 1 out of 10.</param>
    /// <param name="MaxValue">the max value, or a static value that's always higher than the proportion.</param>
    /// <param name="multiplier">the multiplier by how of which the percentage will work, make value as one to only get the proportion.</param>
    /// <returns></returns>
    public static float ConvertToPercentage(float portion, float MaxValue, int multiplier = 100) 
    {
        return portion / MaxValue * multiplier;
    }
}