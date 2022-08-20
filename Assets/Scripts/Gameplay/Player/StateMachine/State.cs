using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Tooltip("Список переходов из текущего состояния")]
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