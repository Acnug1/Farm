using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private const string FirstStateErrorMessage = "FirstState is null";
 
    [Tooltip("Первое состояние в StateMachine, которое должен принять посетитель")]
    [SerializeField] private State _firstState;

    private State _currentState;
    private PlayerAnimatorController _playerAnimatorController;

    public State CurrentState => _currentState;

    private void OnDisable()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState = null;
        }
    }

    private void Start()
    {
        Debug.Assert(_firstState != null, FirstStateErrorMessage);
        _playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();
    }

    private void Update()
    {
        if (_firstState == null)
            return;

        if (_currentState == null)
            Reset(_firstState);

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State firstState)
    {
        _currentState = firstState;

        if (_currentState != null)
            _currentState.Enter(_playerAnimatorController);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_playerAnimatorController);
    }
}
