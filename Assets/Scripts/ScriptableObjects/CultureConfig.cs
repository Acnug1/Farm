using UnityEngine;

[CreateAssetMenu(fileName = "New CultureConfig", menuName = "Culture/Create new CultureConfig", order = 51)]

public class CultureConfig : ScriptableObject
{
    [Tooltip("������� �������� ��� ����� �������� �� ��� Y")]
    [Range(0.1f, 1f)]
    [SerializeField] private float _targetScaleY = 1f;
    [Tooltip("����� ����� ��������")]
    [Min(1f)]
    [SerializeField] private float _growthTime = 10f;

    public float TargetScaleY => _targetScaleY;
    public float GrowthTime => _growthTime;
}
