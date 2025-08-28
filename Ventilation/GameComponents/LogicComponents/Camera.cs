using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Logic;
public enum CameraStates 
{
    Fixed,
    Lerped,
    None
}
public sealed class Camera
{
    private CameraStates camState = CameraStates.None;
    private Vector2 camPos = Vector2.Zero, camTarget = Vector2.Zero, offset = Vector2.Zero, screenVector = Vector2.Zero;
    private float rotationDegree = 0;
    private float scale = 1;
    private float lerpSpeed;
    // private fields
    public CameraStates SwitchState(CameraStates newState) => camState = newState;
    
    // matrices
    public Matrix TransformMatrix => Matrix.CreateTranslation(camPos.X - offset.X, camPos.Y - offset.Y, 0);
    public Matrix ScaleMatrix => Matrix.CreateScale(scale, scale, 1);
    public Matrix RotationMatrix => Matrix.CreateRotationZ(rotationDegree);
    public Matrix ViewMatrix1 => ScaleMatrix * RotationMatrix * TransformMatrix;
    public Matrix ViewMatrix2 => TransformMatrix * RotationMatrix * ScaleMatrix;
    
    
    public Vector2 CameraPosition { get { return camPos; } private set { camPos = value; } }
    public Vector2 CameraTarget { get { return camTarget; } private set { camTarget = value; } }
    public Vector2 Offset { get { return offset; } set { offset = value; } }
    
    
    public float RotationDegrees { get { return rotationDegree; } set { rotationDegree = value; } }
    public float Scale { get { return scale; } set { scale = value < 0 ? 0 : value; } }
    public float LerpSpeed { get { return lerpSpeed; } set { lerpSpeed = MathHelper.Clamp(value, 0, 1); } }
    // bools
    public bool CenterOnTarget { get; set; } = false;
    // methods for camera targets
    public void SetTarget(Vector2 location) => camTarget = location;
    public void SetTarget(Rectangle region) => camTarget = new Vector2(region.X, region.Y);
    // methods for camera anchors (moving the camera to an absolute location beyond lerped movements)
    public void Anchor(Vector2 location) => camPos = location;
    public void Anchor(Rectangle region) => camPos = new Vector2(region.X, region.Y);
    // methods for windows
    public void UpdateScreenVector(Vector2 newSize) => screenVector = newSize;
    public void UpdateScreenVector(Rectangle windowBounds) => screenVector = new Vector2(windowBounds.Width, windowBounds.Height);
    
    // helper methods for updating
    private void Fixed() 
    {
        Vector2 TargetPosition = CenterOnTarget ? CameraTarget + (screenVector / 2) : CameraTarget + screenVector;
        CameraPosition = TargetPosition;
    }
    private void Lerped() 
    {
        CameraPosition = CenterOnTarget ? Vector2.Lerp(CameraPosition, CameraTarget + (screenVector / 2), lerpSpeed) :
            Vector2.Lerp(CameraPosition, CameraTarget + screenVector, lerpSpeed);
    }
    public void UpdateLens()
    {
        switch (camState) 
        {
            case CameraStates.None: break;
            case CameraStates.Fixed: Fixed(); break;
            case CameraStates.Lerped: Lerped(); break;
        }
    }
    // constructor(s).
    public Camera(Rectangle ScreenSize, float lerpSpeed = 0.5f, float scale = 1f, float angle = 0) 
    {
        this.screenVector = new(ScreenSize.Width, ScreenSize.Height);
        this.lerpSpeed = lerpSpeed;
        this.scale = scale;
        this.rotationDegree = angle;
    }
    public Camera(Viewport WindowPort, float lerpSpeed = 0.5f, float scale = 1f, float angle = 0) 
    {
        this.screenVector = new(WindowPort.Width, WindowPort.Height);
        this.lerpSpeed = lerpSpeed;
        this.scale = scale;
        this.rotationDegree = angle;
    }
    public Camera(Vector2 ScreenSizes, float lerpSpeed = 0.5f, float scale = 1f, float angle = 0) 
    {
        this.screenVector = ScreenSizes;
        this.lerpSpeed = lerpSpeed;
        this.scale = scale;
        this.rotationDegree = angle;
    }
}