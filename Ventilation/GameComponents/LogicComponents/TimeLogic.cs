using Microsoft.Xna.Framework;
namespace GameComponents.Logic;
public class Timer 
{
    private float elapsedTime; // the current time in runtime
    private readonly float declaredTime; // the time that the Timer started with
    private float timeMulti = 1; // the multiplier, whether to slow down the Timer or speed it up, multiplied by 1 as default
    private float timeInterval; // the interval the Timer ticks down by
    private readonly int[] timeArray; // the list of the amount of seconds to choose from
    private bool isActive = true; // determines whether the Timer is active (running) or not (paused)
    private bool autoRestart = false; // the value that determines whether or not the Timer instantly restarts the moment it hits zero.
    // private fields
    public float ElapsedTime { get { return elapsedTime; } protected set { elapsedTime = MathHelper.Clamp(value, 0f, declaredTime); } }
    public float DeclaredTime => declaredTime;
    public float TimeMultiplier { get { return timeMulti; } set { timeMulti = value; } }
    public float TimerInterval => timeInterval;
    public bool AutoRestart { get { return autoRestart; } set { autoRestart = value; } }
    public int GetTimeFromArray(int value) => timeArray[value <= 0 ? timeArray.Length - 1 : (value - 1) % timeArray.Length];
    public bool IsActive => isActive;
    public void PauseTimer() => isActive = false;
    public void ContinueTimer() => isActive = true;
    public bool TimerIsZero() => elapsedTime <= 0;
    public void RestartTimer() => ElapsedTime = DeclaredTime;
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
        if (autoRestart && elapsedTime <= 0) 
        {
            elapsedTime = declaredTime;
        }
        else if (!autoRestart && elapsedTime <= 0) 
        {
            elapsedTime = 0;
        } // back
    }
}