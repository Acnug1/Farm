using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    private const string TargetStateErrorMessage = "TargetState is null";

    [Tooltip("������� ���������, ������� ������������ ����� ��������� ��������")]
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }
    protected PlayerAnimatorController PlayerAnimatorController { get; private set; }
    protected State CurrentState { get; private set; }

    private void Awake()
    {
        Debug.Assert(_targetState != null, TargetStateErrorMessage);

        OnTransitionAwake();
    }

    private void OnEnable()
    {
        NeedTransit = false;
        OnTransitionEnter();
    }

    public void Init(PlayerAnimatorController playerAnimatorController, State currentState)
    {
        PlayerAnimatorController = playerAnimatorController;
        CurrentState = currentState;
    }

    /// <summary>
    /// ����� OnTransitionAwake, ������� ���������� ��� ������������� ��������
    /// </summary>
    protected virtual void OnTransitionAwake() { }

    /// <summary>
    /// ����� OnTransitionEnter, ������� ���������� ��� ��������� ��������
    /// </summary>
    protected virtual void OnTransitionEnter() { }

    /// <summary>
    /// ����� Update, ������� ���������� ������, ����� ���� ������� �������
    /// </summary>
    protected virtual void Update() { }
}