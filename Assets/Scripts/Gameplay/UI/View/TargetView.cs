using TMPro;
using UnityEngine;

[DefaultExecutionOrder(600)]

public class TargetView : MonoBehaviour
{
    private const string TargetCounterErrorMessage = "TargetCounter is null";

    [Tooltip("—сылка на счетчик цели")]
    [SerializeField] private TMP_Text _targetCounter;

    private GameOver _gameOver;

    private void Awake()
    {
        Debug.Assert(_targetCounter != null, TargetCounterErrorMessage);
    }

    private void Start()
    {
        _gameOver = LevelLoaderInstance.Instance.GetComponent<GameOver>();

        if (!_gameOver)
            throw new MissingComponentException();

        _targetCounter.text = _gameOver.Target.ToString();
    }
}
