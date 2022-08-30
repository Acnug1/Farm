using UnityEngine;

[CreateAssetMenu(fileName = "New CultureConfig", menuName = "Culture/Create new CultureConfig", order = 51)]

public class CultureConfig : ScriptableObject
{
    [Tooltip("����� ����� ������� ��������")]
    [Min(0f)]
    [SerializeField] private float _timeBetweenSpawn = 5f;
    [Tooltip("������� �������� ��� ����� �������� �� ��� Y")]
    [Range(0.1f, 1f)]
    [SerializeField] private float _targetScaleY = 1f;
    [Tooltip("����� ����� ��������")]
    [Min(1f)]
    [SerializeField] private float _growthTime = 10f;
    [Tooltip("������ �� ������ ��������, ������� ������ �����")]
    [SerializeField] private Plant _plantPrefab;

    public float TimeBetweenSpawn => _timeBetweenSpawn;
    public float TargetScaleY => _targetScaleY;
    public float GrowthTime => _growthTime;
    public Plant PlantPrefab => _plantPrefab;
}
