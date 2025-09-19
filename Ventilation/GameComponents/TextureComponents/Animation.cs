using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation
{
    // private fields
    private bool _isPlaying = true;
    private int currentFrameIndex;
    private float frameTime = 1;
    private int startingIndex = 0;
    private int endingIndex = 0;
    private float deltaTime;
    // public properties
    public readonly TextureAtlas SpriteSheet;
    public Rectangle[] FrameGallery => SpriteSheet.Regions;
    public bool IsLooping { get; set; } = true;
    public bool IsReversed { get; set; } = false;
    public int CurrentFrameIndex 
    {
        get => currentFrameIndex;
        private set => currentFrameIndex = MathHelper.Clamp(value, startingIndex, endingIndex);
    }
    public int StartingIndex 
    {
        get => startingIndex;
        set => startingIndex = value >= endingIndex ? endingIndex - 1 : value;
    }
    public int EndingIndex 
    {
        get => endingIndex;
        set => endingIndex = value <= startingIndex ? startingIndex + 1 : value;
    }
    public float FrameTime { get { return frameTime; } set { frameTime = MathHelper.Clamp(value, 0, 1); } }
    public float DeltaTime => deltaTime;
    public Rectangle CurrentFrame => FrameGallery[currentFrameIndex];
    // helper stuff
    public float FPS { get { return 1 / frameTime; } set { FrameTime = value < 0 ? 0 : 1 / value; } }
    public float NormalizedProgress => MathHelper.Clamp(DeltaTime / FrameTime, 0f, 1f);
    // methods
    public void Play() => _isPlaying = true;
    public void Stop() => _isPlaying = false;
    public void GoTo(int newFrame) => CurrentFrameIndex = newFrame;
    public void Reset(bool hardReset = false) 
    {
        if (hardReset)
        {
            CurrentFrameIndex = 0;
            deltaTime = 0;
        }
        else deltaTime = 0;
    }
    // constructors
    /// <summary>
    /// a constructor for the Animation class.
    /// </summary>
    /// <param name="sheet">the required parameter to get the TextureAtlas for the frames.</param>
    /// <param name="start">the startingIndex of when the frame starts, set to 0 if you ant the very beginning of the spriteSheet to be the first frame.</param>
    /// <param name="end">the ending Index of wher the animation ends, set to 0 to set the default max value as the very end of the sprite sheet.</param>
    public Animation(ref TextureAtlas sheet, int start = 0, int end = 0) 
    {
        SpriteSheet = sheet;
        StartingIndex = start;
        if (end == 0) EndingIndex = sheet.TileAmount;
        else EndingIndex = end;
    }
    public Animation(TextureAtlas sheet, int start = 0, int end = 0) 
    {
        SpriteSheet = sheet;
        StartingIndex = start;
        if (end == 0) EndingIndex = sheet.TileAmount;
        else EndingIndex = end;
    }
    // the update method(s).
    private void Reversed(GameTime gt) 
    {
        deltaTime -= (float)gt.ElapsedGameTime.TotalSeconds;
        if (deltaTime <= 0) 
        {
            CurrentFrameIndex--;
            deltaTime = frameTime;
        }
        if (IsLooping && currentFrameIndex <= startingIndex) 
        {
            CurrentFrameIndex = endingIndex;
        } else if (!IsLooping && currentFrameIndex <= startingIndex) 
        {
            CurrentFrameIndex = startingIndex;
        }
    }
    private void Normal(GameTime gt) 
    {
        deltaTime += (float)gt.ElapsedGameTime.TotalSeconds;
        if (deltaTime >= frameTime) 
        {
            CurrentFrameIndex++;
            deltaTime = 0;
        }
        if (IsLooping && currentFrameIndex >= endingIndex) 
        {
            CurrentFrameIndex = 0;
        } else if (!IsLooping && currentFrameIndex >= endingIndex) 
        {
            CurrentFrameIndex = endingIndex;
        }
    }
    public void Roll(GameTime gt) // heheh? get it? roll the TAPES BAAAAAAAAAAAAAAAAAAAAAAAAAAAAHHAHAHAHAHA *wheezing noises*
    {
        if (!_isPlaying) return;
        if (IsReversed) Reversed(gt);
        else Normal(gt);
    }
    public void Scroll(SpriteBatch batch, Rectangle bounds, Color color) 
    {
        batch.Draw(SpriteSheet.Atlas, bounds, CurrentFrame, color);
    }
    public void Scroll(SpriteBatch batch, Vector2 position, Color color) 
    {
        batch.Draw(SpriteSheet.Atlas, position, CurrentFrame, color);
    }
    public void Scroll(SpriteBatch batch, Sprite sprite, Rectangle bounds) 
    {
        batch.Draw(SpriteSheet.Atlas, bounds, CurrentFrame, sprite.Color, sprite.Radians, sprite.Origin, sprite.Effects, sprite.LayerDepth);
    }
    public void Scroll(SpriteBatch batch, Sprite sprite, Vector2 position) 
    {
        batch.Draw(SpriteSheet.Atlas, position, CurrentFrame, sprite.Color, sprite.Radians, sprite.Origin, sprite.Scale, sprite.Effects, sprite.LayerDepth);
    }
}