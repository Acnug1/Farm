using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Список конфигов игровых уровней
/// Содержит индекс текущего уровня
/// </summary>
[CreateAssetMenu(fileName = "New LevelsList", menuName = "Settings/Create new LevelsList", order = 51)]

public class LevelsList : StaticScriptableObject<LevelsList>
{
    private const string LevelIndex = "LevelIndex";

    [Tooltip("Игровые уровни")]
    [SerializeField] private List<GameObject> _levels = new List<GameObject>();

    /// <summary>
    /// Индекс текущего уровня
    /// </summary>
    public int CurrentLevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndex, 0);
        set
        {
            value = Mathf.Max(0, value);

            // Если уровни закончились - начинаем с первого
            if (value >= _levels.Count) 
                value = 0;

            PlayerPrefs.SetInt(LevelIndex, value);
        }
    }

    /// <summary>
    /// Префаб текущего уровня
    /// </summary>
    public GameObject CurrentLevel
    {
        get
        {
            return _levels[CurrentLevelIndex];
        }
    }

    /// <summary>
    /// Проверить корректность списка уровней
    /// </summary>
    /// <returns></returns>
    public bool CheckAsserts()
    {
        if (_levels.Count == 0) 
            Debug.LogError("Не указан ни один уровень в " + name);

        if (_levels.Find(level => level == null) != null) 
            Debug.LogError("Не указан один из уровней в " + name);

        var knownKeys = new HashSet<string>();

        if (_levels.Any(level => !knownKeys.Add(level.name))) 
            Debug.LogError("Повторяющиеся уровни в " + name);

        return true;
    }

    /// <summary>
    /// Загрузить уровень, который задан активным (выполняется только при запуске приложения)
    /// </summary>
    public void LoadActiveLevel()
    {
        if (Application.isPlaying)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Сбросить игровой прогресс
    /// </summary>
    [Button]
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();

        LoadActiveLevel();
    }

    /// <summary>
    /// Установить уровень с указанным именем в качестве текущего уровня
    /// </summary>
    /// <param name="levelName"></param>
    [Button]
    public void SetCurrentLevel(string levelName)
    {
        int levelIndex = _levels.FindIndex(level => level.name == levelName);

        Debug.Assert(levelIndex >= 0, "Невозможно в списке уровней найти уровень - " + levelName);

        CurrentLevelIndex = levelIndex;

        LoadActiveLevel();
    }

    /// <summary>
    /// Перейти к следующему уровню
    /// </summary>
    [Button]
    public void NextLevel()
    {
        CurrentLevelIndex++;

        LoadActiveLevel();
    }

    /// <summary>
    /// Перейти к предыдущему уровню
    /// </summary>
    [Button]
    public void PreviousLevel()
    {
        CurrentLevelIndex--;

        LoadActiveLevel();
    }
}
