using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
namespace GameComponents.Rendering;
public class TextureAtlas : ITextureAtlas // class, dedicated for sprite sheets or TileAtlases.
{
    private Texture2D tileSheet;
    // private fields
    public Texture2D TextureSheet => tileSheet;
    public Dictionary<int, Rectangle> Region { get; }
    public Rectangle AtlasBounds => tileSheet.Bounds;
    public Vector2 TileSize => new Vector2(TileWidth, TileHeight);
    public readonly int TileWidth;
    public readonly int TileHeight;
    public readonly int TotalFrames;
    public readonly int Rows;
    public readonly int Columns;
    public TextureAtlas(Texture2D texture, int columns, int rows) 
    {
        tileSheet = texture;

        Rows = rows;
        Columns = columns;
        TotalFrames = rows * columns;

        TileWidth = texture.Bounds.X / columns;
        TileHeight = texture.Bounds.Y / rows;

        Region = new Dictionary<int, Rectangle>(TotalFrames);
        for(int frame = 0; frame < TotalFrames; frame++) 
        {
            int x = frame / Columns + 1;
            int y = frame / Rows + 1;
            
            Region.Add(frame, new Rectangle(x, y, TileWidth, TileHeight));
        }
    }
    
}