using Microsoft.Xna.Framework;
using GameComponents;
using System;
namespace GameComponents.Logic;
public class Camera 
{
    public enum CameraStates 
    {
        Fixed,
        Lerped,
        None
    }
    private CameraStates CamState = CameraStates.None;
    private Matrix transformMatrix = new(), rotationMatrix = new(), scaleMatrix = new();
    
    
}