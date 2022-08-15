using UnityEngine;

[DefaultExecutionOrder(300)]

public class UnitAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _moveSpeedId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponentInParent<Rigidbody>();

        _moveSpeedId = Animator.StringToHash("MoveSpeed");
    }

    private void Update()
    {
        _animator.SetFloat(_moveSpeedId, _rigidbody.velocity.magnitude);
    }
}
