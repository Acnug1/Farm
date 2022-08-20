using UnityEngine;

[DefaultExecutionOrder(300)]
[RequireComponent(typeof(Animator))]

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private int _moveSpeedId;
    private int _cutId;

    public float LengthOfCurrentClip => _animator.GetCurrentAnimatorStateInfo(0).length;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _moveSpeedId = Animator.StringToHash("MoveSpeed");
        _cutId = Animator.StringToHash("Cut");
    }

    public void SpeedChanged(float speed)
    {
        _animator.SetFloat(_moveSpeedId, speed);
    }

    public void Cut()
    {
        _animator.SetTrigger(_cutId);
    }

    public void ResetCut()
    {
        _animator.ResetTrigger(_cutId);
    }
}
