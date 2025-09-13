namespace GameComponents;
public enum Actions 
{
    Ready, // Action is waiting...
    Charging, // Action is winding up or winding down.
    Active, // Action is fully active
    Completed, // action has successfully completed.
    Cooldown, // action is in recovery duration.
    Interrupted, // action is interrupted.
    Ended, // action ended.
    Disabled, // action is disabled.
}
