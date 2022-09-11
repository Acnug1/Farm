using UnityEngine;

[CreateAssetMenu(fileName = "New CoinsSpawnerConfig", menuName = "Coins/Create new CoinsSpawnerConfig", order = 51)]

public class CoinsSpawnerConfig : ScriptableObject
{
    [Tooltip("������ �� ������ �������")]
    [SerializeField] private Coin _coinPrefab;

    public Coin CoinPrefab => _coinPrefab;
}
