using System;
using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Logic;
public sealed class Raycast : IDirection
{
    private Vector2 direction = Vector2.UnitX;
    private Vector2 origin = Vector2.Zero;
    private float maxDistance = 0;
    private float t = 0;
    // private fields
    public Vector2 Direction { get => direction; set => direction = Vector2.Normalize(value); }
    public Vector2 Perpendicular => new Vector2(-Direction.Y, Direction.X);
    public Vector2 Origin { get => origin; set => origin = value; }
    public float HitDistance => t;
    public float MaxDistance { get => maxDistance; set => maxDistance = Math.Abs(value); }
    public float Radians 
    {
        get => (float)Math.Atan2(Direction.Y, Direction.X);
        set => Direction = new Vector2((float)Math.Cos(value), (float)Math.Sin(value));
    }
    public Vector2 GetEndPoints() => origin + (direction * maxDistance);
    // methods
    public void LookAt(Vector2 location) => Direction = location - origin;
    public void LookAt(Point location) => Direction = new Vector2(location.X, location.Y) - origin;

    public void LookLike(Vector2 direction) => Direction = direction;
    public void LookLike(Point direction) => Direction = new Vector2(direction.X, direction.Y);
    
    public float Dot(Vector2 direction) 
    {
        return Vector2.Dot(direction, Direction);
    }
    // collision
    public bool RayToSegment(Vector2 start, Vector2 end) 
    {
        Vector2 segDir = end - start;
        Vector2 perp = new Vector2(-segDir.Y, segDir.X);
        float DotProduct = Dot(perp);
        float u = Vector2.Dot(Perpendicular, start - Origin) / DotProduct;
        float calculatedT = Vector2.Dot(perp, start - Origin) / DotProduct;
        bool IsValid = u >= 0 && u <= 1;
        if (Math.Abs(DotProduct) < 0.0001f) return false;
        if (IsValid && calculatedT >= 0 && calculatedT <= maxDistance)
        {
            t = calculatedT;
            return true;
        }
        else { t = maxDistance; return false; }
    }
    public bool RayToRectangle(Rectangle bounds) 
    {
        Vector2[] Vertices = new Vector2[4];
        Vertices[0] = new Vector2(bounds.Left, bounds.Top);
        Vertices[1] = new Vector2(bounds.Right, bounds.Top);
        Vertices[2] = new Vector2(bounds.Right, bounds.Bottom);
        Vertices[3] = new Vector2(bounds.Left, bounds.Bottom);
        for(int i = 0; i < 4; i++) 
        {
            Vector2 start = Vertices[i];
            Vector2 end = Vertices[(i + 1) % 4];
            Vector2 segDir = end - start;
            Vector2 perp = new Vector2(-segDir.Y, segDir.X);
            float dot = Dot(perp);
            float calculatedT = Vector2.Dot(perp, start - Origin) / dot;
            float U = Vector2.Dot(Perpendicular, start - Origin) / dot;
            bool IsValid = U >= 0 && U <= 1;
            if (Math.Abs(dot) < 0.0001f) continue;
            if (IsValid && calculatedT >= 0 && calculatedT <= maxDistance) 
            {
                t = calculatedT;
                return true;
            } else { t = maxDistance;  return false; }
        }
        t = maxDistance;
        return false;
    }
    public void DrawLine(SpriteBatch batch, Texture2D texture, Color color, float thickness = 1) 
    {
        batch.Draw(texture, Origin, null, color, Radians, new Vector2(0.5f, 0.5f), new Vector2(thickness, maxDistance), SpriteEffects.None, 1);
    }
}