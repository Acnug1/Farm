using UnityEngine;

[CreateAssetMenu(fileName = "New CoinConfig", menuName = "Coins/Create new CoinConfig", order = 51)]

public class CoinConfig : ScriptableObject
{
    [Tooltip("�����, �� ������� ������� ��������� ����� ����������")]
    [Min(0.01f)]
    [SerializeField] private float _timeToDestinationPoint = 1f;

    public float TimeToDestinationPoint => _timeToDestinationPoint;
}
