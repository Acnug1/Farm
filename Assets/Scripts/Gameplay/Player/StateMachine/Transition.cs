using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    private const string TargetStateErrorMessage = "TargetState is null";
   
    [Tooltip("Целевое состояние, которое активируется после успешного перехода")]
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        Debug.Assert(_targetState != null, TargetStateErrorMessage);
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}