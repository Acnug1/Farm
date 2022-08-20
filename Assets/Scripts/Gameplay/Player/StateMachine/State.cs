using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Tooltip("������ ��������� �� �������� ���������")]
    [SerializeField] private List<Transition> _transitions;

    protected PlayerAnimatorController PlayerAnimatorController { get; private set; }

    private void OnEnable()
    {
        OnStateEnter();
    }

    private void OnDisable()
    {
        OnStateExit();
    }

    public void Enter(PlayerAnimatorController playerAnimatorController)
    {
        if (enabled == false)
        {
            PlayerAnimatorController = playerAnimatorController;

            enabled = true;

            foreach (Transition transition in _transitions)
            {
                transition.Init(playerAnimatorController, this);
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (Transition transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    protected virtual void Awake() { }

    /// <summary>
    /// ����������, ����� ��� ��������� ���������� ��������
    /// </summary>
    protected virtual void OnStateEnter() { }

    /// <summary>
    /// ���������� ��� ������ �� ����� ���������
    /// </summary>
    protected virtual void OnStateExit() { }

    /// <summary>
    /// ����� Update, ������� ���������� ������, ����� ��� ��������� �������
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// ����� FixedUpdate, ������� ���������� ������, ����� ��� ��������� �������
    /// </summary>
    protected virtual void FixedUpdate() { }
}