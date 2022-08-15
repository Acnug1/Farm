using InControl;
using UnityEngine;

[DefaultExecutionOrder(100)]

public class PlayerInput : MonoBehaviour
{
    public bool TouchPressed => InputManager.ActiveDevice.Action1.WasPressed;
}
