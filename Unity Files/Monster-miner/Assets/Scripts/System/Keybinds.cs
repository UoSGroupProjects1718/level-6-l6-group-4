//Oliver
using UnityEngine;

public class Keybinds : SingletonClass<Keybinds>
{
    [Header("Camera Movement")]
    public KeyCode MoveCameraLeft;
    public KeyCode MoveCameraRight;
    public KeyCode MoveCameraUp;
    public KeyCode MoveCameraDown;
    public KeyCode RotateCameraLeft;
    public KeyCode RotateCameraRight;
    [Header("Interaction buttons")]
    public KeyCode PrimaryActionKey;
    public KeyCode SecondaryActionKey;
    public KeyCode UnitMiltiSelect;
    public KeyCode CancelButton;
    public KeyCode BuildingRotationKey;

}
