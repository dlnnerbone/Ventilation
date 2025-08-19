using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Managers;
public abstract class GameManager 
{
    public virtual void Initialize(Game game) {}
    public virtual void LoadContent(Game game) {}
    public virtual void UnloadContent(Game game) {}
    public virtual void UpdateLogic(GameTime GT) {}
    public virtual void Draw(SpriteBatch batch) {}
}