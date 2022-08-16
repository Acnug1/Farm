using UnityEngine;

[DefaultExecutionOrder(300)]
[RequireComponent(typeof(Animator))]

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;
    private int _moveSpeedId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponentInParent<PlayerMove>();

        _moveSpeedId = Animator.StringToHash("MoveSpeed");
    }

    private void OnEnable()
    {
        _playerMove.SpeedChanged += OnSpeedChanged;
    }

    private void OnDisable()
    {
        _playerMove.SpeedChanged -= OnSpeedChanged;
    }

    private void OnSpeedChanged(float speed)
    {
        _animator.SetFloat(_moveSpeedId, speed);
    }
}
