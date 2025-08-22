using Microsoft.Xna.Framework;
using System;
using GameComponents.Entity;
namespace GameComponents;
[Flags] public enum TileType 
{
    None = 0,
    Collidable = 1,
    Walkable = 2,
    Dangerous = 4,
    Movable = 8
}
public struct Tile 
{
    private Rectangle sourceRegion;
    private int tileID;
    // private stuff;
    public Rectangle SourceRegion { get { return sourceRegion; } private set { sourceRegion = value; } }
    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);
    public int TileID { get { return tileID; } set { tileID = value < 0 ? 0 : value; } }
    public int X { get { return sourceRegion.X; } set { sourceRegion.X = value; } }
    public int Y { get { return sourceRegion.Y; } set { sourceRegion.Y = value; } }
    public Vector2 Location 
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
    public float Left => sourceRegion.Left;
    public float Right => sourceRegion.Right;
    public float Bottom => sourceRegion.Bottom;
    public float Top => sourceRegion.Top;

    // bools
    public TileType TileTypes { get; set; }
    public bool IsCollidable => (TileTypes & TileType.Collidable) == TileType.Collidable;
    public bool IsMovable => (TileTypes & TileType.Movable) == TileType.Movable;
    public bool IsDangerous => (TileTypes & TileType.Dangerous) == TileType.Dangerous;
    public bool IsWalkable => (TileTypes & TileType.Walkable) == TileType.Walkable;
    public bool IntersectsWithTile(Rectangle other) => sourceRegion.Intersects(other);
    public bool IntersectsWithTile(BodyComponent other) => sourceRegion.Intersects(other.Bounds);
    public Tile(int x, int y, int width, int height, int ID) 
    {
        sourceRegion = new(x, y, width, height);
        TileID = ID;
    }
}