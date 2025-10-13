using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameComponents.Rendering;
using GameComponents.Managers;
namespace Main;
public class FPSChecker 
{
    private float fps;
    private SpriteText spriteText;
    private InputManager input = new();
    private bool _canDrawFps => input.IsKeyDown(Keys.Tab);
    
    public void LoadFPSFont(ContentManager content) 
    {
        spriteText = new(content.Load<SpriteFont>("GameAssets/SpriteFonts/PixelatedElegance"), new Vector2(1920 - 100, 50));
        spriteText.Color = spriteText.DColor;
        spriteText.Scale = new(2, 2);
    }
    
    public void GetFPS(GameTime gt) 
    {
        input.UpdateInputs();
        fps = 1 / (float)gt.ElapsedGameTime.TotalSeconds;
    }
    
    public void DrawFPS(SpriteBatch batch) 
    {
        if (!_canDrawFps) return;
        else spriteText.DrawString(batch, $"{Math.Ceiling(fps)}");
    }
}