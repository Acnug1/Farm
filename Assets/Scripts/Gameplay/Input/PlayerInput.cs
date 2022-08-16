using UnityEngine;
using InControl;

[DefaultExecutionOrder(100)]

public class PlayerInput : MonoBehaviour
{
    public Vector2 Direction { get; private set; }

    private void Update()
    {
        Direction = InputManager.ActiveDevice.Direction.Value;
    }
}
