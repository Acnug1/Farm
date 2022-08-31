using UnityEngine;

[DefaultExecutionOrder(200)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SurfaceSlider))]
[RequireComponent(typeof(NavAgent))]

public class PlayerMove : MonoBehaviour
{
    private const string PlayerInputErrorMessage = "PlayerInput is null";
    private const string PlayerMoveConfigErrorMessage = "PlayerMoveConfig is null";

    [Tooltip("—сылка на PlayerInput")]
    [SerializeField] private PlayerInput _playerInput;
    [Tooltip("—сылка на ScriptableObject: PlayerMoveConfig")]
    [SerializeField] private PlayerMoveConfig _playerMoveConfig;

    private Rigidbody _rigidbody;
    private SurfaceSlider _surfaceSlider;
    private NavAgent _navAgent;
    private PlayerAnimatorController _playerAnimatorController;
    private Vector3 _direction;
    private float _speed;
    private LayerMask _layerMask;

    private void Awake()
    {
        Debug.Assert(_playerInput != null, PlayerInputErrorMessage);
        Debug.Assert(_playerMoveConfig != null, PlayerMoveConfigErrorMessage);

        _rigidbody = GetComponent<Rigidbody>();
        _surfaceSlider = GetComponent<SurfaceSlider>();
        _navAgent = GetComponent<NavAgent>();
        _playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();

        _speed = _playerMoveConfig.Speed;
        _layerMask = _playerMoveConfig.LayerMask;
    }

    private void FixedUpdate()
    {
        _direction = GetDirection(_playerInput.Direction);

        Vector3 directionAlongSurface = _surfaceSlider.GetDirectionAlongSurface(_direction, _layerMask);

        if (_direction != Vector3.zero)
        {
            Move(directionAlongSurface, _speed);
            Rotate(directionAlongSurface);
        }
        else
            Stop();
    }

    private Vector3 GetDirection(Vector2 direction)
    {
        return Vector3.forward * direction.y + Vector3.right * direction.x;
    }

    private void Move(Vector3 direction, float speed)
    {
        _navAgent.Move(direction, speed);
        _playerAnimatorController.SpeedChanged(speed);
    }

    private void Rotate(Vector3 direction)
    {
        _rigidbody.rotation = Quaternion.LookRotation(direction);
    }

    private void Stop()
    {
        _navAgent.Stop();
        _playerAnimatorController.SpeedChanged(0);
    }
}
