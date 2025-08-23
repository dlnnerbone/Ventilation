using Microsoft.Xna.Framework;
using System;
using GameComponents.Entity;
namespace GameComponents;
[Flags] public enum TileFlags 
{
    None = 0,
    Collidable = 1,
    Walkable = 2,
    Dangerous = 4,
    Movable = 8
}
public readonly struct Tile 
{
    // fields
    public readonly Rectangle Region;
    public readonly int TileID;
    public readonly TileFlags Flags;
    
    // constructors
    public Tile(int x, int y, int width, int height, int ID, TileFlags flag = TileFlags.None) 
    {
        Region = new(x, y, width, height);
        TileID = ID < 0 ? 0 : ID;
        Flags = flag;
    }
    private Tile(Rectangle Bounds, int ID, TileFlags flag) 
    {
        Region = Bounds;
        TileID = ID;
        Flags = flag;
    }

    // utility properties
    public Vector2 TopLeft => new Vector2(Region.Left, Region.Top);
    public Vector2 TopRight => new Vector2(Region.Right, Region.Top);
    public Vector2 BottomLeft => new Vector2(Region.Left, Region.Bottom);
    public Vector2 BottomRight => new Vector2(Region.Right, Region.Bottom);
    public Vector2 Center => new Vector2(Region.X + Region.Width / 2, Region.Y + Region.Height / 2);
    public Vector2 HalfSize => new Vector2(Region.Width / 2, Region.Height / 2);

    // -- flag checks --
    public bool IsMovable => (Flags & TileFlags.Movable) == TileFlags.Movable;
    public bool IsCollidable => (Flags & TileFlags.Collidable) == TileFlags.Collidable;
    public bool IsDangerous => (Flags & TileFlags.Dangerous) == TileFlags.Dangerous;
    public bool IsWalkable => (Flags & TileFlags.Walkable) == TileFlags.Walkable;
    public bool HasNoAttributes => Flags == TileFlags.None;

    // safe modifiers
    public Tile AddFlag(TileFlags newFlag) => new Tile(this.Region, this.TileID, this.Flags | newFlag);
    public Tile RemoveFlag(TileFlags flag) => new Tile(this.Region, this.TileID, this.Flags & ~flag);
    public Tile OverrideFlags(TileFlags FlagGroup) => new Tile(this.Region, this.TileID, FlagGroup);
    public Tile PurgeFlags() => new Tile(this.Region, this.TileID, TileFlags.None);

    // intersection methods
    public bool IntersectsWithTile(Rectangle other) => Region.Intersects(other);
    public bool IntersectsWithTile(BodyComponent other) => Region.Intersects(other.Bounds);
    
}