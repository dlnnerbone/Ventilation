using Microsoft.Xna.Framework;
namespace GameComponents.Logic;
public class Timer 
{
    private float elapsedTime;
    private readonly float declaredTime;
    private float timeMulti = 1;
    private float timeInterval;
    private readonly int[] timeArray;
    private bool isActive = false;
    private bool autoRestart = false;
    // private fields
    public float ElapsedTime { get { return elapsedTime; } protected set { elapsedTime = MathHelper.Clamp(value, 0f, declaredTime); } }
    public float DeclaredTime => declaredTime;
    public float TimeMultiplier { get { return timeMulti; } set { timeMulti = value; } }
    public float TimerInterval => timeInterval;
    public int GetTimeFromArray(int value) => timeArray[value <= 0 ? timeArray.Length - 1 : (value - 1) % timeArray.Length];
    public bool IsActive => isActive;
    public void Deactivate() => isActive = false;
    public void Activate() => isActive = true;
    public void AutoRestartOnZero() => autoRestart = true;
    public void DisableAutoRestart() => autoRestart = false;
    public Timer(float seconds) 
    {
        elapsedTime = seconds;
        declaredTime = seconds;
        for (int i = 0; i <= seconds; i++) timeArray = new int[i];
    }
    public virtual void UpdateTimer(GameTime gt) 
    {
        timeInterval = (float)gt.ElapsedGameTime.TotalSeconds * timeMulti;
        if (isActive) elapsedTime -= timeInterval;
        if (autoRestart && elapsedTime <= 0) elapsedTime = declaredTime;
        else if (!autoRestart && elapsedTime <= 0) elapsedTime = 0;
    }
}