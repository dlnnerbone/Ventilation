using System;
using Microsoft.Xna.Framework;
namespace GameComponents.Logic;
public enum TimeStates 
{
    Up, // counting Up
    Down // countind Down
}
public class Timer 
{
    /// <summary>
    /// A Timer class for instiating simple Timer mechanics, Cooldowns, Durations, Etc, can be easily implemeneted here.
    /// </summary>
    private float timeSpan;
    private float timeInterval = 0;
    private float timeMulti = 1;
    private float duration;
    private TimeStates tState;
    // private fields
    public float TimeSpan { get { return timeSpan; } set { timeSpan = MathHelper.Clamp(value, 0, duration); } }
    public float TimeMultiplier { get { return timeMulti; } set { timeMulti = value < 0 ? 0 : value; } }
    public float Duration { get { return duration; } set { duration = value < 0.0001f ? 0.0001f : value; } }
    public bool IsLooping { get; set; }
    public bool IsPaused { get; set; }
    
    // helper methods
    public TimeStates SwitchTimeState(TimeStates newState) => tState = newState;
    public bool IsCountingUp() => tState == TimeStates.Up;
    public bool IsCountingDown() => tState == TimeStates.Down;
    public float CeilingSpan => (float)Math.Ceiling(TimeSpan);
    public float FloorSpan => (float)Math.Floor(timeSpan);
    public float NormalizedProgress => MathHelper.Clamp(timeSpan / duration, 0f, 1f);
    
    public bool TimerHitsTarget 
    {
        get => IsCountingUp() && timeSpan >= duration ? true : IsCountingDown() && timeSpan <= 0 ? true : false;
    }
    public void Restart() 
    {
        if (IsCountingDown()) TimeSpan = duration;
        else if (IsCountingUp()) TimeSpan = 0;
    }
    // constructors
    public Timer(float seconds, float duration, TimeStates tState = TimeStates.Down, bool isLooping = false, bool isPaused = false) 
    {
        if (seconds < 0 || duration < 0.0001f) throw new ArgumentException("duration and/or seconds can not have values below zero.");
        this.tState = tState;
        this.duration = duration;
        timeSpan = seconds;
        IsLooping = isLooping;
        IsPaused = isPaused;
    }
    public Timer(float seconds, TimeStates tState = TimeStates.Down, bool isLooping = false, bool isPaused = false) 
    {
        if (seconds < 0) throw new ArgumentException("seconds can not have a value below zero.");
        this.tState = tState;
        duration = seconds;
        timeSpan = seconds;
        IsLooping = isLooping;
        IsPaused = isPaused;
    }
    // main update methods
    private void Down(GameTime gt) 
    {
        timeSpan -= timeInterval;
        if (IsLooping && timeSpan <= 0) 
        {
            timeSpan = duration;
        }
        else if (!IsLooping && timeSpan <= 0) 
        {
            timeSpan = 0;
        }
    }
    private void Up(GameTime gt) 
    {
        timeSpan += timeInterval;
        if (IsLooping && timeSpan >= duration) 
        {
            timeSpan = 0;
        }
        else if (!IsLooping && timeSpan >= duration) 
        {
            timeSpan = duration;
        }
    }
    public void TickTock(GameTime gt) 
    {
        timeInterval = (float)gt.ElapsedGameTime.TotalSeconds * timeMulti;
        if (IsPaused) return;
        switch (tState) 
        {
            case TimeStates.Up: Up(gt); break;
            case TimeStates.Down: Down(gt); break;
        }
    }
} 
