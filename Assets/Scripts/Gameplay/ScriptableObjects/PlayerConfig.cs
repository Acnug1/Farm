using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Player/Create new PlayerConfig", order = 51)]


public class PlayerConfig : ScriptableObject
{
    [Tooltip("Максимальное число стаков урожая, которое может подбирать игрок")]
    [Min(1f)]
    [SerializeField] private int _maxCropCount = 40;

    public int MaxCropCount => _maxCropCount;
}
