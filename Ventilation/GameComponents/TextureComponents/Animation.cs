using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation
{
    // public properties
    public readonly TextureAtlas SpriteSheet; // the spritesheet itself.
    public Dictionary<int, Rectangle> Frames => SpriteSheet.Regions; // a getter to get the SpriteSHeet's regions/frames.
    public bool IsLooping { get; set; } = true; // if the animation loops or not.
    public float FrameDelay { get { return frameDelay; } set { frameDelay = MathHelper.Clamp(value, 0f, 1f); } } // how long each frame is displayed before moving on.
    public float FPS => 1 / FrameDelay;
    public float CurrentTime => currentTime;
    public int Start { get { return start; } set { start = value > end ? end - 1 : value; } }
    public int End { get { return end; } set { end = value < start ? start + 1 : value; } }
    public Rectangle Frame => Frames[currentFrameIndex];
    // internal properties
    private bool isPlaying = true; // whether or not the animation is playing.
    private float frameDelay = 1;
    private int currentFrameIndex; // the current frame in ID being selected, bounded by start and end points.
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
    public Animation(TextureAtlas spriteSheet, int start, int end = 0, float frameDelay = 1) 
    {
        SpriteSheet = spriteSheet;
        
        currentFrameIndex = this.start = start > end ? start - 1 : start;
        
        this.end = MathHelper.Clamp(end, start, spriteSheet.Regions.Count);
        
        this.frameDelay = frameDelay;
        
        currentTime = frameDelay;
    }
    public void Animate(GameTime gt) 
    {
        if (!isPlaying) return;
        currentTime += (float)gt.ElapsedGameTime.TotalSeconds;
        currentFrameIndex = MathHelper.Clamp(currentFrameIndex, Start, End);
        if (currentTime >= frameDelay) 
        {
            currentTime = 0;
            currentFrameIndex++;
        }
        if (currentFrameIndex >= End && IsLooping) 
        {
            currentFrameIndex = Start;
        } else if (currentFrameIndex >= End && !IsLooping) 
        {
            currentFrameIndex = End;
        }
    }
    public void Draw(SpriteBatch batch, Rectangle Bounds) 
    {
        batch.Draw(SpriteSheet.Atlas, Bounds, Frame, Color.White);
    }
}