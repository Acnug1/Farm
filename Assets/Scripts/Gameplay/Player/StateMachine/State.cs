using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Tooltip("Список переходов из текущего состояния")]
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
    /// Вызывается, когда это состояние становится активным
    /// </summary>
    protected virtual void OnStateEnter() { }

    /// <summary>
    /// Вызывается при выходе из этого состояния
    /// </summary>
    protected virtual void OnStateExit() { }

    /// <summary>
    /// Метод Update, который вызывается только, когда это состояние активно
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// Метод FixedUpdate, который вызывается только, когда это состояние активно
    /// </summary>
    protected virtual void FixedUpdate() { }
}