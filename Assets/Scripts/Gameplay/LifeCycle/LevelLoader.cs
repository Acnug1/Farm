using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GameOver))]

/// <summary>
/// ��������� ������ �������� ������
/// </summary>
public class LevelLoader : MonoBehaviour
{
    private GameOver _gameOver;
    private LevelsList _levelsList;

    public event UnityAction VictoryMenuOpening;
    public event UnityAction DefeatMenuOpening;

    private void Awake()
    {
        _gameOver = GetComponent<GameOver>();
        _levelsList = LevelsList.Instance;

        if (_levelsList.CheckAsserts())
        {
            // ��������� ������ ������
            Time.timeScale = 1f;
            Instantiate(_levelsList.CurrentLevel);
        }

        _gameOver.Victory += OnVictory;
        _gameOver.Defeat += OnDefeat;
    }

    private void OnDestroy()
    {
        _gameOver.Victory -= OnVictory;
        _gameOver.Defeat -= OnDefeat;
    }

    private void OnVictory()
    {
        // ��������� �� ��������� �������
        _levelsList.CurrentLevelIndex++;
        VictoryMenuOpening?.Invoke();
    }

    private void OnDefeat()
    {
        DefeatMenuOpening?.Invoke();
    }
}
