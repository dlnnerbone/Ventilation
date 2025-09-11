using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
namespace GameComponents.Rendering;
public class Sprite : ITexture 
{
    private Texture2D texture;
    private Color[] colors;
    private Vector2 scale = Vector2.One;
    private Vector2 origin = Vector2.Zero;
    private float radians = 0;
    private float depth = 0;
    private SpriteEffects effects = SpriteEffects.None;
    
    // private fields.
    public Texture2D Texture { get { return texture; } set { texture = value; } }
    public Rectangle Bounds => texture.Bounds;
    public Vector2 Scale { get { return scale; } set { scale = value; } }
    public Vector2 Origin { get { return origin; } set { origin = value; } }
    public SpriteEffects Effects => effects;
    public Color[] Colors => colors;
    public Color Color { get { return colors[0]; } set { colors[0] = value; } }
    public float R { get { return Color.R; } set { Color = new(value, Color.G, Color.B, Color.A); } }
    public float G { get { return Color.G; } set { Color = new(Color.R, value, Color.B, Color.A); } }
    public float B { get { return Color.B; } set { Color = new(Color.R, Color.B, value, Color.A); } }
    public float Opacity { get { return Color.A; } set { Color = new(Color.R, Color.G, Color.B, value); } }
    public float LayerDepth { get { return depth; } set { depth = MathHelper.Clamp(value, 0f, 1f); } }
    public float Radians { get { return radians; } set { radians = MathHelper.ToRadians(value); } }
    public Sprite(Texture2D texture, Color selectedColor) 
    {
        this.texture = texture;
        colors = new Color[] { selectedColor };
    }
    public bool Flip_H { set { effects = value == true ? effects |= SpriteEffects.FlipHorizontally : effects &= ~SpriteEffects.FlipHorizontally; } }
    public bool Flip_V { set { effects = value == true ? effects |= SpriteEffects.FlipVertically : effects &= ~SpriteEffects.FlipVertically; } }
    public void FlipBackToNormal() => effects = SpriteEffects.None;
    public void SetToData() => texture.SetData<Color>(Colors);
    public void Draw(SpriteBatch batch, Rectangle Destination, Rectangle Source) 
    {
        batch.Draw(Texture, Destination, Source, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Rectangle Destination) 
    {
        batch.Draw(Texture, Destination, null, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 Destination, Rectangle Source) 
    {
        batch.Draw(Texture, Destination, Source, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 Destination) 
    {
        batch.Draw(Texture, Destination, null, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    // extra overloads for convienance
    public void Draw(SpriteBatch batch, Rectangle Destination, Rectangle Source, float angle) 
    {
        batch.Draw(Texture, Destination, Source, Color, angle, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Rectangle Destination, float angle) 
    {
        batch.Draw(Texture, Destination, null, Color, angle, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination, Rectangle source, float angle) 
    {
        batch.Draw(Texture, destination, source, Color, angle, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination, float angle) 
    {
        batch.Draw(Texture, destination, null, Color, angle, Origin, Scale, Effects, LayerDepth);
    }
}