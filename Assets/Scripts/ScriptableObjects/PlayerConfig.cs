using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Player/Create new PlayerConfig", order = 51)]

public class PlayerConfig : ScriptableObject
{
    [Tooltip("—корость передвижени€ игрока")]
    [Min(1f)]
    [SerializeField] private float _speed = 5f;
    [Tooltip("ћаска слоев, по которым может перемещатьс€ игрок")]
    [SerializeField] private LayerMask _layerMask;

    public float Speed => _speed;
    public LayerMask LayerMask => _layerMask;
}
