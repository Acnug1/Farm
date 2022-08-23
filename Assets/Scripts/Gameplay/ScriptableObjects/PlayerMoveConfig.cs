using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerMoveConfig", menuName = "Player/Create new PlayerMoveConfig", order = 51)]

public class PlayerMoveConfig : ScriptableObject
{
    [Tooltip("—корость передвижени€ игрока")]
    [Min(1f)]
    [SerializeField] private float _speed = 3f;
    [Tooltip("ћаска слоев, по которым может перемещатьс€ игрок")]
    [SerializeField] private LayerMask _layerMask;

    public float Speed => _speed;
    public LayerMask LayerMask => _layerMask;
}
