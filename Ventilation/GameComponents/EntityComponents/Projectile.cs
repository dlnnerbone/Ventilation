using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Entity;
public class Projectile : BodyComponent
{
    protected float scalarSpeed = 0;
    protected Vector2 direction = Vector2.One;
    private Actions actionState = Actions.Ready;
    // private fields
    public virtual float ScalarSpeed { get { return scalarSpeed; } set { scalarSpeed = Math.Abs(value); } }
    public virtual Vector2 Direction { get { return direction; } set { direction = Vector2.Normalize(value); } }
    public Actions ActionMode(Actions newState) => actionState = newState;
}