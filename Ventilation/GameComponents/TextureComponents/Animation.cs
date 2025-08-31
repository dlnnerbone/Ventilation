using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation
{
    // public properties
    public readonly TextureAtlas SpriteSheet; // the spritesheet itself.
    public Dictionary<int, Rectangle> Frames => SpriteSheet.Regions; // a getter to get the SpriteSHeet's regions/frames.
    public bool IsLooping { get; set; } = true; // if the animation loops or not.
    public float FrameDelay { get; private set; } // how long each frame is displayed before moving on.
    public float CurrentTime => currentTime;
    public int Start { get { return start; } set { start = value > end ? end - 1 : value; } }
    public int End { get { return end; } set { end = value < start ? start + 1 : value; } }
    // internal properties
    private bool isPlaying = true; // whether or not the animation is playing.
    private int currentFrameIndex; // the current frame in ID being selected
    private float currentTime; // the time since the last frame had been displayed.
    private int start;
    private int end;
    // methods (Animation playing)
    public void Play()
    {
        isPlaying = true;
    }
    public void Stop()
    {
        isPlaying = false;
    }

    // constructor(s)
    public Animation(TextureAtlas spriteSheet, int start = 0, int end = 0) 
    {
        SpriteSheet = spriteSheet;
        this.start = start > end ? end - 1 : start;
        if (end == 0) this.end = spriteSheet.Regions.Count;
        else if (end < start) this.end = start + 1;

        currentFrameIndex = start;
        
    }
}