using Microsoft.Xna.Framework;
namespace GameComponents;

public struct Tile 
{
    private Rectangle sourceRegion;
    // private stuff;
    public Rectangle SourceRegion { get { return sourceRegion; } private set { sourceRegion = value; } }
    public int TileID;
    public int X { get { return sourceRegion.X; } set { sourceRegion.X = value; } }
    public int Y { get { return sourceRegion.Y; } set { sourceRegion.Y = value; } }
    public Vector2 Position 
    {
        get => new Vector2(X, Y);
        set => sourceRegion.Location = new((int)value.X, (int)value.Y);
    }
    public int Width { get { return sourceRegion.Width; } set { sourceRegion.Width = value; } }
    public int Height { get { return sourceRegion.Height; } set { sourceRegion.Height = value; } }
    public Vector2 Size 
    {
        get => new Vector2(Width, Height);
        set => sourceRegion.Size = new((int)value.X, (int)value.Y);
    }
}