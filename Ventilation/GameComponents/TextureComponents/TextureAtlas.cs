using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public readonly struct TextureAtlas 
{
    public readonly Texture2D Atlas;
    public readonly Dictionary<int, Rectangle> Regions;
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
        
        for(int r = 0; r < rows; r++) 
        {
            for(int c = 0; c < columns; c++) 
            {
                int x = c * TileWidth;
                int y = r * TileHeight;
                int index = r * Columns + c;
                Regions[index] = new(x, y, TileWidth, TileHeight);
            }
        }
    }
}