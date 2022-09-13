using UnityEngine;
using InControl;

[DefaultExecutionOrder(100)]

public class PlayerInput : MonoBehaviour
{
    public Vector2 Direction => InputManager.ActiveDevice.Direction.Value;
}
