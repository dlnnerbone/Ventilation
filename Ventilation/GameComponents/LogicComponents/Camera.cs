using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Logic;
public enum CameraStates 
{
    Fixed,
    Lerped,
    None
}
public sealed class Camera 
{
    private CameraStates state = CameraStates.None;
    private Matrix transformMatrix = new(), rotationMatrix = new(), scaleMatrix = new();
    private Vector2 cameraPos = Vector2.Zero, offset = Vector2.Zero, target = Vector2.Zero, screenSize;
    private float scale = 0;
    private float radians = 0;
    private float speed = 0.5f;
    // private fields
    public CameraStates SwitchStates(CameraStates camState) => state = camState;
    public Matrix TransformMatrix { get { return transformMatrix; } }
    public Matrix RotationMatrix { get { return rotationMatrix; } }
    public Matrix ScaleMatrix { get { return scaleMatrix; } }
    public Vector2 CameraPosition { get { return cameraPos; } private set { cameraPos = value; } }
    public Vector2 Offset { get { return offset; } set { offset = value; } }
    public Vector2 CameraTarget { get { return target; } private set { target = value; } }
    public float ScaleFactor { get { return scale; } set { scale = MathHelper.Clamp(value, 0, 10); } }
    public float Angle { get { return radians; } set { radians = MathHelper.ToRadians(value); } }
    public float Speed { get { return speed; } set { speed = MathHelper.Clamp(value, 0, 1); } }
    public Camera(Vector2 Screen, float scale = 1, float radians = 0, float speed = 0.5f) 
    {
        this.screenSize = Screen / 2;
        this.scale = scale;
        this.radians = radians;
        this.speed = speed;
    }
    public Camera(Rectangle screenBounds, float scale = 1, float radians = 0, float speed = 0.5f) 
    {
        this.screenSize = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
        this.scale = scale;
        this.radians = radians;
        this.speed = speed;
    }
    public void SetTarget(Vector2 target) => this.target = -target;
    public void SetTarget(Rectangle bounds) => this.target = new Vector2(bounds.X, bounds.Y);
    // constructor and camera target methods.
    public void NewScreenSize(Vector2 Screen) => this.screenSize = Screen / 2;
    public void NewScreenSize(Rectangle bounds) => this.screenSize = new Vector2(bounds.Width / 2, bounds.Height / 2);
    public void UpdateCamera() 
    {
        rotationMatrix = Matrix.CreateRotationZ(Angle);
        scaleMatrix = Matrix.CreateScale(ScaleFactor, ScaleFactor, 0);
        switch (state) 
        {
            case CameraStates.None: break;
            case CameraStates.Fixed: Fixed(); break;
            case CameraStates.Lerped: Lerped(); break;
        }
    }
    private void Fixed() 
    {
        CameraPosition = CameraTarget + screenSize;
        transformMatrix = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 1);
    }
    private void Lerped() {}
}