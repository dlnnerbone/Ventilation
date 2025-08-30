using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation 
{
    private float framesPerSecond; // speed of iteration
    private int start; // the frame that starts first (limit)
    private int end; // when the animation ends and repeats itself from the start.
    private int frameID; // the ID the currentFrame follos by.
    // private fields;
    public bool Repeat { get; set; } = true; // determines whether the animation automatically repeats or not when it reaches the end.
    public bool Pause { get; set; } = false; // determines whether the animation pauses or not. (stops iterating).
    public readonly TextureAtlas SpriteSheet; // the SpriteSheet containing all the frames
    public float FPS { get { return framesPerSecond; } set { framesPerSecond = MathHelper.Clamp(value, 0, Math.Abs(value)); } }
    public float TimePerFrame => 1 / FPS; // the time in a floated value how much seconds it takes for switching to the next frame.
    public int Start { get { return start; } set { start = value > end ? end - 1 : value; } }
    public int End { get { return end; } set { end = value < start ? start + 1 : value; } }
    public Rectangle CurrentFrame => SpriteSheet.Regions[frameID]; // the CurrentFrame the Sprite encompasses.
    public int CurrentFrameID { get { return frameID; } set { frameID = MathHelper.Clamp(value, Start, End); } } // the current frame ID
    // constructors
    /// <summary>
    ///  the constructor for creating the Animation and the start/end parameters. FrameID's default is the startInt.
    /// </summary>
    /// <param name="spriteSheet">the PNg file or image containg each frame of the spriteSheet. remember that the rows and columns are separate to the start and end ints.</param>
    /// <param name="startInt">the int of which the first frame of the animation starts or begins at.</param>
    /// <param name="endInt">this is the limit of the animation when the frames iterate and reach the end. either starts at the beginning int or stops.</param>
    /// <param name="FPS">the parameter to select how fast this animation can be, the more the faster, the lower the slower.</param>
    public Animation(TextureAtlas spriteSheet, int startInt, int endInt, float FPS = 5) 
    {
        SpriteSheet = spriteSheet;
        start = startInt;
        end = endInt;
        framesPerSecond = FPS;

        frameID = MathHelper.Clamp(frameID, startInt, endInt);
    }
    public void UpdateAnimation(GameTime gt) 
    {
        if (!Pause) 
    }
}