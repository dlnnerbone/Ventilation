using Microsoft.Xna.Framework;
namespace GameComponents.Managers;
public static class WindowManager 
{
    public static void ChangeResolution(GraphicsDeviceManager device, int width, int height) 
    {
        device.PreferredBackBufferWidth = width;
        device.PreferredBackBufferHeight = height;
        device.ApplyChanges();
    }
    public static void SetTitle(Game game, string title) => game.Window.Title = title;
}