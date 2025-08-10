using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Logic;
public class Camera 
{
    private CameraStates CamState = CameraStates.None;
    private Matrix transformMatrix = new(), rotationMatrix = new(), scaleMatrix = new();
    private Vector2 cameraPosition = Vector2.Zero, cameraVelocity = Vector2.Zero, offset = Vector2.Zero, cameraTarget = Vector2.Zero;
    private float LerpTemperature;
    private float radians;
    private float scale;
    private float zaxis = 0f;
    private Vector2 HalfSize = Vector2.Zero;
    // private floats
    public CameraStates SwitchStates(CameraStates newState) => CamState = newState;
    public CameraStates GetState() => CamState;
    public Matrix TransformMatrix { get { return transformMatrix; }  }
    public Matrix RotationMatrix { get { return rotationMatrix; } }
    public Matrix ScaleMatrix { get { return scaleMatrix; } }
    public Vector2 CameraPosition { get { return cameraPosition; } private set { cameraPosition = value; } }
    public Vector2 CameraVelocity { get { return cameraVelocity; } private set { cameraVelocity = value; } }
    public Vector2 CameraTarget { get { return cameraTarget; } }
    public Vector2 Offset { get { return offset; } set { offset = value; } }
    public float OffsetX { get { return offset.X; } set { offset.X = value; } }
    public float OffsetY { get { return offset.Y; } set { offset.Y = value; } }
    public float LerpTemp { get { return LerpTemperature; } set { LerpTemperature = MathHelper.Clamp(value, 0f, 1f); } }
    public float Radians { get { return radians; } set { radians = MathHelper.ToRadians(value); } }
    public float Scale { get { return scale; } set { scale = MathHelper.Clamp(value, 0f, 10f); } }
    public float Z_Depth { get { return zaxis; } set { zaxis = MathHelper.Clamp(value, 0f, 1f); } }
    public Camera(float LerpFactor = 1f, float scaleFactor = 1f, float radians = 0f) 
    {
        LerpTemperature = LerpFactor;
        scale = scaleFactor;
        this.radians = radians;
    }
    public void CreateScreenTarget(Vector2 ScreenResolution) => HalfSize = ScreenResolution / 2;
    public void SetTarget(Vector2 cameraTarget) => this.cameraTarget = cameraTarget;
    public void UpdateCamera(GameTime GT) 
    {
        CameraPosition += CameraVelocity * (float)GT.ElapsedGameTime.TotalSeconds;
        transformMatrix = Matrix.CreateTranslation(cameraPosition.X + Offset.X - HalfSize.X, cameraPosition.Y + Offset.Y - HalfSize.Y, Z_Depth);
        scaleMatrix = Matrix.CreateScale(Scale, Scale, Z_Depth);
        rotationMatrix = Matrix.CreateRotationZ(Radians);
        switch(CamState) 
        {
            case CameraStates.None: break;
            case CameraStates.Lerped: Lerped(); break;
            case CameraStates.Fixed: Fixed(); break;
            case CameraStates.Inverted: Inverted(); break;
        }
    }
    private void Fixed() 
    {
        CameraPosition = -cameraTarget;
    }
    private void Lerped() 
    {
        Vector2 delta = -CameraTarget - CameraPosition;
        CameraVelocity = delta * LerpTemp;
    }
    private void Inverted() 
    {
        
    }
    
}