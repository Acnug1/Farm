using UnityEngine;

/// <summary>
/// ����� ��� "������������" ���������� ScriptableObject, ������� ������ ��������� � ����� Resources/Settings.
/// ����� ����������� �� StaticScriptableObject ����� �������� � ���� � ������� ������ Instance.
/// </summary>
/// <typeparam name="T">��� ������������ ������ (��� ������ ������ ���� �� ����������, ��� �������� � ����������� ��������)</typeparam>
public class StaticScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    /// <summary>
    /// �������� ������ �� ��������� ScriptableObject
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                string path = $"Settings/{typeof(T).Name}";
                _instance = Resources.Load(path) as T;
            }

            return _instance;
        }
    }
}