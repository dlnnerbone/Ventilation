using System;
namespace GameComponents;
public enum Actions 
{
    Ready, // No Actions or in a static state.
    Fly, // an action is initialted, (e.g: dashing, shooting, Projectile motion, etc)
    End // an action has ended, or end an action.
}
