using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Interfaces;
public interface ITexture 
{
    public Texture2D Texture { get; set; }
    public Color[] Colors { get; }
}