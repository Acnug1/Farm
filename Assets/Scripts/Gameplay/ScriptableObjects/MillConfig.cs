using UnityEngine;

[CreateAssetMenu(fileName = "New MillConfig", menuName = "Mill/Create new MillConfig", order = 51)]

public class MillConfig : ScriptableObject
{
    [Tooltip("Время, которое должно пройти до полного оборота вращения лопастей у мельницы")]
    [Min(1f)]
    [SerializeField] private float _timeToFullRotation = 10f;

    public float TimeToFullRotation => _timeToFullRotation;
}
