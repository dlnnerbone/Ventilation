namespace GameComponents;
public enum Actions 
{
    Ready, // Action is waiting...
    Charging, // Action is winding up or winding down.
    Active, // Action is fully active
    Interrupted, // action is interrupted.
    Disabled, // action is disabled.
    Transitioning, // action is transaitioning...
    Ended, // action ended.
    Completed, // action has successfully completed.
    Cooldown // action is in recovery duration.
}
