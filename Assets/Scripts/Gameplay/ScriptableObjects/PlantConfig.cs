using UnityEngine;

[CreateAssetMenu(fileName = "New PlantConfig", menuName = "Plant/Create new PlantConfig", order = 51)]

public class PlantConfig : ScriptableObject
{
    [Tooltip("������������ ������ ��������� �������� 'scale.y' � ��������, ����� ���������� �����")]
    [SerializeField] private AnimationCurve _plantScaleYCurve;
    [Tooltip("����� �� ��������� ��������? (���� ���������� false, �� �������� ��������������� ���� ���)")]
    [SerializeField] private bool _isLoopAnimation = true;
    [Tooltip("��������� ���� �������� ��� ������ �����")]
    [SerializeField] private Color _startColor = Color.yellow;
    [Tooltip("������� ����, ������� ������ ������� �������� ��� ������ �����")]
    [SerializeField] private Color _targetColor = Color.green;
    [Tooltip("������ ����� ��������")]
    [SerializeField] private ParticleSystem _cutEffect;
    [Tooltip("�������� ������ �������, ������������ ������ �������� � ���������� ������� ���������")]
    [SerializeField] private Vector3 _offsetOfSpawnEffect = new Vector3(0, 1, 0);

    public AnimationCurve PlantScaleYCurve => _plantScaleYCurve;
    public bool IsLoopAnimation => _isLoopAnimation;
    public Color StartColor => _startColor;
    public Color TargetColor => _targetColor;
    public ParticleSystem CutEffect => _cutEffect;
    public Vector3 OffsetOfSpawnEffect => _offsetOfSpawnEffect;
}
