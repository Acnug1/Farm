using UnityEngine;

[CreateAssetMenu(fileName = "New MillConfig", menuName = "Mill/Create new MillConfig", order = 51)]

public class MillConfig : ScriptableObject
{
    [Tooltip("¬рем€, которое должно пройти до полного оборота вращени€ лопастей у мельницы (¬ли€ет только на анимацию)")]
    [Min(1f)]
    [SerializeField] private float _timeToFullRotation = 10f;

    public float TimeToFullRotation => _timeToFullRotation;
}
