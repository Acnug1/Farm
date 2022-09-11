using UnityEngine;

[DefaultExecutionOrder(-100)]

/// <summary>
/// ��������� ������ ���������� ����� ����� � ��������, ����� ������������ �� ������ Singleton<T>
/// </summary>
/// <typeparam name="T">��� ������������ ������</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    [Tooltip("������������� ���������������� ����� ��� ��������")]
    [SerializeField] private bool _autoInitializeOnStart = true;

    #pragma warning disable CS0108
    [Tooltip("�� ���������� ������ ��� ������������ �� ������ �����")]
    [SerializeField] private bool DontDestroyOnLoad = false;
    #pragma warning restore CS0108

    /// <summary>
    /// �������� ������ �� ��������� ������
    /// </summary>
    public static T Instance => _instance;

    /// <summary>
    /// ��������� ������������� ������
    /// </summary>
    public static bool Exists => _instance != null;

    /// <summary>
    /// �����, ����������� ��� ������������� ������
    /// </summary>
    public abstract void Initialize();

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            if (this is T)
            {
                _instance = this as T;
                if (DontDestroyOnLoad && Application.isPlaying)
                    DontDestroyOnLoad(gameObject);
            }
        }
        else if (Application.isPlaying)
        {
            Debug.LogWarning($"[Singleton] Instance {typeof(T)} already exists. Destroying {name}...");
            DestroyImmediate(gameObject);
        }
    }

    protected virtual void Start()
    {
        if (_autoInitializeOnStart)
            Initialize();
    }
}
