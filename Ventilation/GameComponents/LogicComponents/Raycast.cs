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
    /*public bool IntersectsWithRectangle(Rectangle bounds, out float hitT) 
    {
        Vector2 boxMin = new Vector2(bounds.X, bounds.Y);
        Vector2 boxMax = new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height);
        var tMin = float.NegativeInfinity;
        var tMax = float.PositiveInfinity;
        float t1 = (boxMin.X - Origin.X) / Direction.X;
        float t2 = (boxMin.Y - Origin.Y) / Direction.Y;
        float t3 = (boxMax.X - Origin.X) / Direction.X;
        float t4 = (boxMax.Y - Origin.Y) / Direction.Y;

        var tx_near = (float)Math.Min(boxMin.X, boxMax.X);
        var tx_far = (float)Math.Max(boxMin.Y, boxMax.Y);
        var ty_near = (float)Math.Min()
    } */
}