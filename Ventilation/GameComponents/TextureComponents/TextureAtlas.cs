using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public readonly struct TextureAtlas 
{
    public readonly Texture2D Atlas;
    public readonly Rectangle[] Regions; // int is basically the key, and using that key returns the value (The Rectangle)
    public readonly int Rows;
    public readonly int Columns;
    public readonly int TileWidth;
    public readonly int TileHeight;
    // readonly properties
    public Vector2 TileDimensions => new Vector2(TileWidth, TileHeight);
    public int TileAmount => Columns * Rows;
    public TextureAtlas(Sprite atlas, int columns, int rows) 
    {
        Atlas = atlas.Texture;
        Regions = new Rectangle[columns * rows];
        Columns = columns;
        Rows = rows;

        TileWidth = atlas.Bounds.Width / columns;
        TileHeight = atlas.Bounds.Height / rows;
        
        for(int i = 0; i < columns * rows; i++) 
        {
            int x = i % columns * TileWidth;
            int y = i / columns * TileHeight;
            Regions[i] = new Rectangle(x, y, TileWidth, TileHeight);
        }
    }
    public TextureAtlas(Texture2D atlas, int columns, int rows) 
    {
        Atlas = atlas;
        Regions = new Rectangle[columns * rows];
        Columns = columns;
        Rows = rows;

        TileWidth = atlas.Bounds.Width / columns;
        TileHeight = atlas.Bounds.Height / rows;
        
        for(int i = 0; i < columns * rows; i++) 
        {
            int x = i % columns * TileWidth;
            int y = i / columns * TileHeight;
            Regions[i] = new Rectangle(x, y, TileWidth, TileHeight);
        }
    }
}