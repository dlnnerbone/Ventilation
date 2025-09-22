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
    private Vector2 screenSize = Vector2.Zero;
    // private stuff
    // Matrices
    public Matrix TransformMatrix => Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0);
    public Matrix RotationMatrix => Matrix.CreateRotationZ(Radians);
    public Matrix ScaleMatrix => Matrix.CreateScale(Scale, Scale, 0);
    public Matrix WorldMatrix => RotationMatrix * ScaleMatrix * TransformMatrix;
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
    public float MinZoom { get; set; } = 0.1f;
    public float MaxZoom { get; set; } = 2f;
    public float Scale { get => scale; set => scale = MathHelper.Clamp(Math.Abs(value), MinZoom, MaxZoom); }
    public float EaseLevel { get => easeFactor; set => easeFactor = MathHelper.Clamp(value, 0f, 1f); } 
    public float OffsetX { get => offset.X; set => offset.X = value; }
    public float OffsetY { get => offset.Y; set => offset.Y = value; }
    public float TargetX => CameraPosition.X;
    public float TargetY => CameraPosition.Y;
    public float X => CameraPosition.X;
    public float Y => CameraPosition.Y;
    // booleans
    public bool IsActive { get; set; }
    public bool CenterOnTarget { get; set; }
    // methods
    public void Anchor(Vector2 location) => CameraPosition = location;
    public void Anchor(Point location) => CameraPosition = new Vector2(location.X, location.Y);

    public void SetTarget(Vector2 target) => CameraTarget = target;
    public void SetTarget(Point target) => CameraTarget = new Vector2(target.X, target.Y);

    public void ResizeViewport(Rectangle size) => screenSize = new Vector2(size.Width, size.Height);
    public void ResizeViewport(Viewport size) => screenSize = new Vector2(size.Width, size.Height);
    // Rotation methods
    public void FaceAt(Vector2 location) => Direction = location - CameraPosition;
    public void FaceAt(Point location) => Direction = new Vector2(location.X, location.Y) - CameraPosition;

    public void FaceLike(Vector2 direction) => Direction = direction;
    public void FaceLike(Point direction) => Direction = new Vector2(direction.X, direction.Y);
    
    public Vector2 ScreenToWorld(Vector2 screenPos) 
    {
        return Vector2.Transform(screenPos, Matrix.Invert(WorldMatrix));
    }
    public Vector2 WorldToScreen(Vector2 worldPos) 
    {
        return Vector2.Transform(worldPos, WorldMatrix);
    }
    // main Update method
    public void Recording() 
    {
        if (!IsActive) return;
        var targetPos = CenterOnTarget ? CameraTarget + Offset - screenSize :
            CameraTarget + Offset;
        CameraPosition = Vector2.LerpPrecise(CameraPosition, targetPos, EaseLevel);
    }
    // constructors
    public Camera(Rectangle viewport, bool CenterOnTarget = true, float scale = 1f, float minZoom = 0.1f, float maxZoom = 2f, float easeLvl = 1f) 
    {
        screenSize = new Vector2(viewport.Width, viewport.Height);
        this.CenterOnTarget = CenterOnTarget;
        Scale = scale;
        MinZoom = minZoom;
        MaxZoom = maxZoom;
        EaseLevel = easeLvl;
    }
    public Camera(Viewport viewport, bool CenterOnTarget = true, float scale = 1f, float minZoom = 0.1f, float maxZoom = 2f, float easeLvl = 1f) 
    {
        screenSize = new Vector2(viewport.Width, viewport.Height);
        this.CenterOnTarget = CenterOnTarget;
        Scale = scale;
        MinZoom = minZoom;
        MaxZoom = maxZoom;
        EaseLevel = easeLvl;
    }
}