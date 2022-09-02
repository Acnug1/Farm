using UnityEngine;

[CreateAssetMenu(fileName = "New CropConfig", menuName = "Crop/Create new CropConfig", order = 51)]

public class CropConfig : ScriptableObject
{
    [Tooltip("�������� �������� ����� ������, ��� ����� � ���������")]
    [Min(1f)]
    [SerializeField] private float _moveSpeedToContainer = 10f;
    [Tooltip("�������� �������� ����� ������, ��� ����� � ���������")]
    [Min(1f)]
    [SerializeField] private float _rotateSpeedToContainer = 720f;
    [Tooltip("�������� ����� ������ �� ��� Y, ��� ���������� � ����������")]
    [Min(0.1f)]
    [SerializeField] private float _offsetYInContainer = 0.1f;

    public float MoveSpeedToContainer => _moveSpeedToContainer;
    public float RotateSpeedToContainer => _rotateSpeedToContainer;
    public float OffsetYInContainer => _offsetYInContainer;
}
