using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public readonly struct TextureAtlas 
{
    public readonly Texture2D Atlas;
    public readonly Dictionary<int, Rectangle> Regions; // int is basically the key, and using that key returns the value (The Rectangle)
    public readonly int Rows;
    public readonly int Columns;
    public readonly int TileWidth;
    public readonly int TileHeight;
    // readonly properties
    public Vector2 TileDimensions => new Vector2(TileWidth, TileHeight);
    public int TileAmount => Regions.Count;
    public TextureAtlas(Texture2D atlas, int columns, int rows) 
    {
        Atlas = atlas;
        Regions = new Dictionary<int, Rectangle>(columns * rows);
        Columns = columns;
        Rows = rows;

        TileWidth = atlas.Bounds.Width / columns;
        TileHeight = atlas.Bounds.Height / rows;
        
        for(int i = 0; i < columns * rows; i++) 
        {
            int c = i % columns;
            int r = i / columns;

            int x = c * TileWidth;
            int y = r * TileHeight;
            Regions[i] = new Rectangle(x, y, TileWidth, TileHeight);

            System.Diagnostics.Debug.WriteLine($"{x}, {y}");
        }
    }
}