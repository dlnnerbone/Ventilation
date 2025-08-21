using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace GameComponents.Interfaces;
public interface ITextureAtlas 
{
    public Texture2D TextureSheet { get; }
    public Dictionary<int, Rectangle> Region { get; }
}