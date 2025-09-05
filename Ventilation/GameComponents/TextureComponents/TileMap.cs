using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents;
namespace GameComponents.Rendering;
public sealed class TileMaps 
{
    public readonly TextureAtlas TileSheet;
    public int TotalTiles => Columns * Rows;
    public readonly int Rows;
    public readonly int Columns;
    public Vector2 Length => new Vector2(Columns, Rows);
    public Vector2 MapSize => Length * TileDimensions;
    public Vector2 TileDimensions => new Vector2(TileWidth, TileHeight); 
    public float TileWidth { get; set; }
    public float TileHeight { get; set; }
    
}