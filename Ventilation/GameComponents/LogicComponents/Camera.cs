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
public class Camera 
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
    public float Scalefactor { get { return scale; } set { scale = MathHelper.Clamp(value, 0, 10); } }
    public float Angle { get { return radians; } set { radians = MathHelper.ToRadians(value); } }
    public float Speed { get { return speed; } set { speed = MathHelper.Clamp(value, 0, 1); } }
}