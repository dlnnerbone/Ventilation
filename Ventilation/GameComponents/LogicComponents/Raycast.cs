using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents;
public class Raycast 
{
    private Vector2 origin;
    private Vector2 direction = Vector2.Zero;
    private float maxDistance = 1000;
    // private fields
    public Vector2 Origin { get { return origin; } private set { origin = value; } }
    public Vector2 Direction { get { return direction; } private set { direction = Vector2.Normalize(value); } }
    public float MaxDistance { get { return maxDistance; } set { maxDistance = value; } }
    public float Radians => (float)Math.Atan2(-Direction.Y, Direction.X);
    public Vector2 GetEndPoints() => maxDistance < 0 ? Origin + (Direction * 1000) : Origin + (Direction * MaxDistance);
    public void LookAt(Vector2 target) => Direction = target - Origin;
    public void NewOrigin(Vector2 location) => Origin = location;
    public bool IntersectsWithLineSegment(Vector2 start, Vector2 end, out float HitT) 
    {
        Vector2 SegDir = end - start;
        Vector2 Perp = new Vector2(-SegDir.Y, SegDir.X);
        float DotProduct = Vector2.Dot(Perp, Direction);
        float calculatedT = Vector2.Dot(Perp, start - Origin) / DotProduct;
        float U = Vector2.Dot(new Vector2(-Direction.Y, Direction.X), start - Origin) / DotProduct;
        bool IsUValid = U >= 0 && U <= 1;
        HitT = calculatedT;
        if (Math.Abs(DotProduct) < 0.0001f) 
        {
            return false;
        }
        if (IsUValid && calculatedT >= 0 && calculatedT <= maxDistance) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool IntersectsWithRectangle(Rectangle bounds, out float hitT) 
    {
        Vector2[] Vertices = new Vector2[4];
        Vertices[0] = new(bounds.Left, bounds.Top);
        Vertices[1] = new(bounds.Right, bounds.Top);
        Vertices[2] = new(bounds.Right, bounds.Bottom);
        Vertices[3] = new(bounds.Left, bounds.Bottom);
        for(int i = 0; i < 4; i++) 
        {
            var p1 = Vertices[i];
            var p2 = Vertices[(i - 1) % 4];
            var segDir = p2 - p1;
            var Perp = new Vector2(-segDir.Y, segDir.X);
            float Dot = Vector2.Dot(Perp, Direction);
            float calculatedT = Vector2.Dot(Perp, p1 - Origin) / Dot;
            float U = Vector2.Dot(new Vector2(-Direction.Y, Direction.X), p1 - Origin) / Dot;
            bool UValid = U >= 0 && U <= 1;
            if (Math.Abs(Dot) <= 0.0001f) 
            {
                hitT = calculatedT;
                return false;
            }
            if (UValid && calculatedT >= 0 && calculatedT <= maxDistance) 
            {
                hitT = calculatedT;
                return true;
            }
            else 
            {
                hitT = calculatedT;
                return false;
            }
        }
        hitT = maxDistance;
        return false;
    }
}