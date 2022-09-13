using UnityEngine;

[CreateAssetMenu(fileName = "New ControlTutorialConfig", menuName = "ControlTutorial/Create new ControlTutorialConfig", order = 51)]

public class ControlTutorialConfig : ScriptableObject
{
    [Tooltip("Время исчезновения панели обучения")]
    [Min(0.5f)]
    [SerializeField] private float _timeOfFade = 0.5f;

    public float TimeOfFade => _timeOfFade;
}
