using UnityEngine;

[DefaultExecutionOrder(-100)]

/// <summary>
/// Позволяет быстро превращать любой класс в синглтон, через наследование от класса Singleton<T>
/// </summary>
/// <typeparam name="T">Имя наследуемого класса</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    [Tooltip("Автоматически инициализировать класс при загрузке")]
    [SerializeField] private bool _autoInitializeOnStart = true;

    #pragma warning disable CS0108
    [Tooltip("Не уничтожать объект при переключении на другую сцену")]
    [SerializeField] private bool DontDestroyOnLoad = false;
    #pragma warning restore CS0108

    /// <summary>
    /// Получить ссылку на экземпляр класса
    /// </summary>
    public static T Instance => _instance;

    /// <summary>
    /// Проверить существование класса
    /// </summary>
    public static bool Exists => _instance != null;

    /// <summary>
    /// Метод, выполняемый при инициализации класса
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
