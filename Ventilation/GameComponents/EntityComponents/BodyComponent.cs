using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class BodyComponent : IBodyComponent 
{
    private Rectangle bounds;
    // private fields.
    public Rectangle Bounds => bounds;
    public Vector2 HalfSize => new Vector2(bounds.Width / 2, bounds.Height / 2);
    public Vector2 Center => new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
    public Vector2 TopLeft => new Vector2(bounds.Left, bounds.Top);
    public Vector2 TopRight => new Vector2(bounds.Right, bounds.Top);
    public Vector2 BottomLeft => new Vector2(bounds.Left, bounds.Bottom);
    public Vector2 BottomRight => new Vector2(bounds.Right, bounds.Bottom);
    public Vector2 Position 
    {
        get => new Vector2(bounds.X, bounds.Y);
        set => bounds.Location = new((int)value.X, (int)value.Y);
    }
    public int X { get { return bounds.X; } set { bounds.X = value; } }
    public int Y { get { return bounds.Y; } set { bounds.Y = value; } }
    public int Width { get { return bounds.Width; } set { bounds.Width = value; } }
    public int Height { get { return bounds.Height; } set { bounds.Height = value; } }
    public float Left => bounds.Left;
    public float Right => bounds.Right;
    public float Bottom => bounds.Bottom;
    public float Top => bounds.Top;
    public bool Intersects(Rectangle other) => bounds.Intersects(other);
    public bool Intersects(BodyComponent other) => bounds.Intersects(other.Bounds);
    public BodyComponent(int x, int y, int width, int height) 
    {
        bounds = new(x, y, width, height);
    }
    public BodyComponent(Point Location, Point Size) 
    {
        bounds = new(Location, Size);
    }
    public BodyComponent(Rectangle bounds) 
    {
        this.bounds = bounds;
    }
    public BodyComponent(Vector2 position, Vector2 sizes) 
    {
        bounds = new((int)position.X, (int)position.Y, (int)sizes.X, (int)sizes.Y);
    }
    /// <summary>
    /// a special constructor for Vector4 parameters, X is Horizontal Position, Y is Vertical position, Z is Width, and  Is HEIGHT.
    /// </summary>
    /// <param name="data"></param>
    public BodyComponent(Vector4 data) 
    {
        bounds = new((int)data.X, (int)data.Y, (int)data.Z, (int)data.W);
    }
    
}