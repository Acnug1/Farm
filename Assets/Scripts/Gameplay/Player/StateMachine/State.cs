using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Tooltip("������ ��������� �� �������� ���������")]
    [SerializeField] private List<Transition> _transitions;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (Transition transition in _transitions)
                transition.enabled = true;

            OnStateEnter();
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (Transition transition in _transitions)
                transition.enabled = false;

            enabled = false;

            OnStateExit();
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