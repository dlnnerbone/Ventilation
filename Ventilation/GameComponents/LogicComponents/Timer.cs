using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace GameComponents.Logic;
public class Timer 
{
    private float declaredT;
    private float timeSpan;
    private float timeInterval;
    private float timeMultiplier = 1;
    private List<int> timeArray;
    // private fields
    public float TimeSpan { get { return timeSpan; } private set { timeSpan = value; } }
    public float ParsedTimeSpan => (float)Math.Ceiling(TimeSpan);
    public float TimeInterval => timeInterval;
    public float TimeMultiplier { get { return timeMultiplier; } set { timeMultiplier = value < 0 ? 0 : value; } }
    public int TimeArray(int s) => timeArray[s == 0 ? 0 : (s - 1) % timeArray.Count];
    public bool AutoRestart { get; set; } = false;
    public bool Pause { get; set; } = false;
    public bool TimerIsZero => timeSpan <= 0;
    // methods
    public void Restart() => TimeSpan = declaredT;
    public void TickTock(GameTime gt) 
    {
        timeInterval = (float)gt.ElapsedGameTime.TotalSeconds * TimeMultiplier;
        if (!Pause) TimeSpan -= timeInterval;

        if (AutoRestart && timeSpan <= 0)
        {
            timeSpan = declaredT;
        }
        else if (!AutoRestart && timeSpan <= 0)
        {
            timeSpan = 0;
        }
    }
    public Timer(float Seconds) 
    {
        timeSpan = Seconds;
        declaredT = Seconds;
        timeArray = new List<int>();
        for (int s = 0; s <= Seconds; s++) timeArray.Add(s);
    }
} 
