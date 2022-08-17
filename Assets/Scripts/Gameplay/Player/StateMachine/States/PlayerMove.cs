using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(200)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SurfaceSlider))]

public class PlayerMove : State
{
    private const string PlayerInputErrorMessage = "PlayerInput is null";
    private const string PlayerConfigErrorMessage = "PlayerConfig is null";

    [Tooltip("—сылка на PlayerInput")]
    [SerializeField] private PlayerInput _playerInput;
    [Tooltip("—сылка на ScriptacleObject: PlayerConfig")]
    [SerializeField] private PlayerConfig _playerConfig;

    private Rigidbody _rigidbody;
    private SurfaceSlider _surfaceSlider;
    private Vector3 _direction;
    private float _speed;
    private LayerMask _layerMask;

    public event UnityAction<float> SpeedChanged;

    private void Awake()
    {
        Debug.Assert(_playerInput != null, PlayerInputErrorMessage);
        Debug.Assert(_playerConfig != null, PlayerConfigErrorMessage);

        _rigidbody = GetComponent<Rigidbody>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        _speed = _playerConfig.Speed;
        _layerMask = _playerConfig.LayerMask;
    }

    protected override void FixedUpdate()
    {
        _direction = GetDirection(_playerInput.Direction);

        Vector3 directionAlongSurface = _surfaceSlider.GetDirectionAlongSurface(_direction, _layerMask);

        if (_direction != Vector3.zero)
        {
            Move(directionAlongSurface);
            Rotate(directionAlongSurface);
        }
        else
            Stop();
    }

    private Vector3 GetDirection(Vector2 direction)
    {
        return Vector3.forward * direction.y + Vector3.right * direction.x;
    }

    private void Move(Vector3 direction)
    {
        Vector3 offset = direction * _speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + offset);
        SpeedChanged?.Invoke(_speed);
    }

    private void Rotate(Vector3 direction)
    {
        _rigidbody.rotation = Quaternion.LookRotation(direction);
    }

    private void Stop()
    {
        SpeedChanged?.Invoke(0);
    }
}
