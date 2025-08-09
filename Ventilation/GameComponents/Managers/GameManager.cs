using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Managers;
public abstract class GameManager 
{
    public abstract void Initialize(Game game);
    public abstract void LoadContent(Game game);
    public virtual void UnloadContent(Game game) {}
    public abstract void UpdateLogic(GameTime GT);
    public abstract void Draw(SpriteBatch batch);
}