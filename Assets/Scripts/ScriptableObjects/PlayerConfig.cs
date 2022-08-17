using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Player/Create new PlayerConfig", order = 51)]

public class PlayerConfig : ScriptableObject
{
    [Tooltip("�������� ������������ ������")]
    [Min(1f)]
    [SerializeField] private float _speed = 5f;
    [Tooltip("����� �����, �� ������� ����� ������������ �����")]
    [SerializeField] private LayerMask _layerMask;

    public float Speed => _speed;
    public LayerMask LayerMask => _layerMask;
}
