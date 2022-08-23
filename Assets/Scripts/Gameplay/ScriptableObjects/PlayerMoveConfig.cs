using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerMoveConfig", menuName = "Player/Create new PlayerMoveConfig", order = 51)]

public class PlayerMoveConfig : ScriptableObject
{
    [Tooltip("�������� ������������ ������")]
    [Min(1f)]
    [SerializeField] private float _speed = 3f;
    [Tooltip("����� �����, �� ������� ����� ������������ �����")]
    [SerializeField] private LayerMask _layerMask;

    public float Speed => _speed;
    public LayerMask LayerMask => _layerMask;
}
