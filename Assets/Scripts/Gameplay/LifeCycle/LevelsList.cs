using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������ �������� ������� �������
/// �������� ������ �������� ������
/// </summary>
[CreateAssetMenu(fileName = "New LevelsList", menuName = "Settings/Create new LevelsList", order = 51)]

public class LevelsList : StaticScriptableObject<LevelsList>
{
    private const string LevelIndex = "LevelIndex";

    [Tooltip("������� ������")]
    [SerializeField] private List<GameObject> _levels = new List<GameObject>();

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    public int CurrentLevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndex, 0);
        set
        {
            value = Mathf.Max(0, value);

            // ���� ������ ����������� - �������� � �������
            if (value >= _levels.Count) 
                value = 0;

            PlayerPrefs.SetInt(LevelIndex, value);
        }
    }

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    public GameObject CurrentLevel
    {
        get
        {
            return _levels[CurrentLevelIndex];
        }
    }

    /// <summary>
    /// ��������� ������������ ������ �������
    /// </summary>
    /// <returns></returns>
    public bool CheckAsserts()
    {
        if (_levels.Count == 0) 
            Debug.LogError("�� ������ �� ���� ������� � " + name);

        if (_levels.Find(level => level == null) != null) 
            Debug.LogError("�� ������ ���� �� ������� � " + name);

        var knownKeys = new HashSet<string>();

        if (_levels.Any(level => !knownKeys.Add(level.name))) 
            Debug.LogError("������������� ������ � " + name);

        return true;
    }

    /// <summary>
    /// ��������� �������, ������� ����� �������� (����������� ������ ��� ������� ����������)
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
    /// �������� ������� ��������
    /// </summary>
    [Button]
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();

        LoadActiveLevel();
    }

    /// <summary>
    /// ���������� ������� � ��������� ������ � �������� �������� ������
    /// </summary>
    /// <param name="levelName"></param>
    [Button]
    public void SetCurrentLevel(string levelName)
    {
        int levelIndex = _levels.FindIndex(level => level.name == levelName);

        Debug.Assert(levelIndex >= 0, "���������� � ������ ������� ����� ������� - " + levelName);

        CurrentLevelIndex = levelIndex;

        LoadActiveLevel();
    }

    /// <summary>
    /// ������� � ���������� ������
    /// </summary>
    [Button]
    public void NextLevel()
    {
        CurrentLevelIndex++;

        LoadActiveLevel();
    }

    /// <summary>
    /// ������� � ����������� ������
    /// </summary>
    [Button]
    public void PreviousLevel()
    {
        CurrentLevelIndex--;

        LoadActiveLevel();
    }
}
