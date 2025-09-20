using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Logic;

public sealed class Camera
{
    private Vector2 camTarget = Vector2.Zero, offset = Vector2.Zero, camPos = Vector2.Zero;
    private Vector2 direction = Vector2.UnitX;
    private float scale = 1f;
    private float easeFactor = 1f;
    private Vector2 _halfScreenSize = Vector2.Zero;
    // private stuff
    // Matrices
    public Matrix TransformMatrix => Matrix.CreateTranslation(CameraPosition.X - _halfScreenSize.X, CameraPosition.Y - _halfScreenSize.Y, 0);
    public Matrix RotationMatrix => Matrix.CreateRotationZ(Radians);
    public Matrix ScaleMatrix => Matrix.CreateScale(Scale);
    public Matrix WorldMatrix1 => RotationMatrix * ScaleMatrix * TransformMatrix;
    public Matrix WorldMatrix2 => TransformMatrix * ScaleMatrix * RotationMatrix;
    // Vector Properties
    public Vector2 CameraTarget { get { return camTarget; } private set { camTarget = value; } }
    public Vector2 Offset { get { return offset; } set { offset = value; } }
    public Vector2 CameraPosition { get { return camPos; } private set { camPos = value; } }
    public Vector2 Direction { get { return direction; } set { direction = Vector2.Normalize(value); } }
    // value variables
    public float Radians
    {
        get => (float)Math.Atan2(Direction.Y, Direction.X);
        set => Direction = new Vector2((float)Math.Cos(value), (float)Math.Sin(value));
    }
    public float Scale { get { return scale; } set { scale = Math.Abs(value); } }
    public float EaseLevel { get { return easeFactor; } set { easeFactor = MathHelper.Clamp(value, 0f, 1f); } }
    // booleans
    public bool IsActive { get; set; } = true;
    public bool CenterOnTarget { get; set; } = true; 
    
}