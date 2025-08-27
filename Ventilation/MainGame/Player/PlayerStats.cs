using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Main;
public sealed class PlayerStats 
{
    public SpriteFont font { get; set; }
    public Vector2 HealthVector { get; set; } = new Vector2(50, 50);
    public Vector2 StaminaVector { get; set; } = new Vector2(50, 120);
    public PlayerStats(ContentManager manager) 
    {
        font = manager.Load<SpriteFont>("GameAssets/SpriteFonts/VCR_EAS");
    }
    public void DrawStats(SpriteBatch batch, Player player) 
    {
        batch.DrawString(font, "Health: " + player.Health, HealthVector, Color.Green);
        batch.DrawString(font, "Stamina: " + player.Stamina, StaminaVector, Color.Blue);
    }
    
}