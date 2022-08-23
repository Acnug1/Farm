using UnityEngine;

[CreateAssetMenu(fileName = "New SliceableObjectConfig", menuName = "SliceableObject/Create new SliceableObjectConfig", order = 51)]

public class SliceableObjectConfig : ScriptableObject
{
    [Tooltip("��������, ������� ������������ � ������ ������� �������")]
    [SerializeField] private Material _sliceMaterial;
    [Tooltip("����� ����� ����������� ���������")]
    [Min(0.5f)]
    [SerializeField] private float _lifeTimeFragment = 1f;
    [Tooltip("����� ������������ ����������� �������")]
    [Min(0.5f)]
    [SerializeField] private float _timeOfDisappearance = 5f;

    public Material SliceMaterial => _sliceMaterial;
    public float LifeTimeFragment => _lifeTimeFragment;
    public float TimeOfDissapearance => _timeOfDisappearance;
}
