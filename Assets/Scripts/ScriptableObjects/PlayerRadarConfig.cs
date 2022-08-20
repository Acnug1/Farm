using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerRadarConfig", menuName = "Player/Create new PlayerRadarConfig", order = 51)]

public class PlayerRadarConfig : ScriptableObject
{
    [Tooltip("���� ���� ��������� ������, ��� ������� �� ����� ����")]
    [Range(0, 180)]
    [SerializeField] private float _detectionTargetAngle = 45f;
    [Tooltip("���� ���� ��������� ������, ��� ������� �� ������ ����")]
    [Range(0, 180)]
    [SerializeField] private float _lostTargetAngle = 45f;

    public float DetectionTargetAngle => _detectionTargetAngle;
    public float LostTargetAngle => _lostTargetAngle;
}
