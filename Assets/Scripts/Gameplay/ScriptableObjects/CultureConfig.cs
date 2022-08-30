using UnityEngine;

[CreateAssetMenu(fileName = "New CultureConfig", menuName = "Culture/Create new CultureConfig", order = 51)]

public class CultureConfig : ScriptableObject
{
    [Tooltip("Время между спавном растений")]
    [Min(0f)]
    [SerializeField] private float _timeBetweenSpawn = 5f;
    [Tooltip("Целевое значение для роста растения по оси Y")]
    [Range(0.1f, 1f)]
    [SerializeField] private float _targetScaleY = 1f;
    [Tooltip("Время роста растения")]
    [Min(1f)]
    [SerializeField] private float _growthTime = 10f;
    [Tooltip("Ссылка на префаб растения, которое должно расти")]
    [SerializeField] private Plant _plantPrefab;

    public float TimeBetweenSpawn => _timeBetweenSpawn;
    public float TargetScaleY => _targetScaleY;
    public float GrowthTime => _growthTime;
    public Plant PlantPrefab => _plantPrefab;
}
