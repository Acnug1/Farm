using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Player/Create new PlayerConfig", order = 51)]


public class PlayerConfig : ScriptableObject
{
    [Tooltip("������������ ����� ������ ������, ������� ����� ��������� �����")]
    [Min(1f)]
    [SerializeField] private int _maxCropCount = 40;
    [Tooltip("�������� � �������� ����� �������� ������� ����� ������")]
    [Min(0.01f)]
    [SerializeField] private float _sellCropDelay = 0.1f;

    public int MaxCropCount => _maxCropCount;
    public float SellCropDelay => _sellCropDelay;
}
