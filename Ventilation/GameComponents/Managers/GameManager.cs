using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Managers;
public abstract class GameManager 
{
    public virtual void Initialize(Game game) {}
    public virtual void LoadContent(GraphicsDevice Device, ContentManager Manager) {}
    public virtual void UnloadContent(ContentManager manager) {}
    public virtual void UpdateLogic(GameTime GT) {}
    public virtual void Draw(SpriteBatch batch) {}
}