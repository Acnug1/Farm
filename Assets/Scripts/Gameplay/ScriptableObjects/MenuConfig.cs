using UnityEngine;

[CreateAssetMenu(fileName = "New MenuConfig", menuName = "Menu/Create new MenuConfig", order = 51)]

public class MenuConfig : ScriptableObject
{
    [Tooltip("Время появления меню")]
    [Min(0.5f)]
    [SerializeField] private float _timeOfAppearance = 0.5f;

    public float TimeOfAppearance => _timeOfAppearance;
}
