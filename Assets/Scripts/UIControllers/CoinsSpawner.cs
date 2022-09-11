using UnityEngine;
using UnityEngine.UI;

public class CoinsSpawner : ObjectPool
{
    private const string CoinsSpawnerConfigErrorMessage = "CoinsSpawnerConfig is null";
    private const string SpawnContainerErrorMessage = "SpawnContainer is null";
    private const string CoinPrefabErrorMessage = "CoinPrefab is null";

    [Tooltip("Ссылка на ScriptableObject: CoinsSpawnerConfig")]
    [SerializeField] private CoinsSpawnerConfig _coinsSpawnerConfig;
    [Tooltip("Контейнер для спавна монеток")]
    [SerializeField] private Transform _spawnContainer;

    private Coin _coinPrefab;
    private Player _player;

    private void Start()
    {
        Debug.Assert(_coinsSpawnerConfig != null, CoinsSpawnerConfigErrorMessage);
        Debug.Assert(_spawnContainer != null, SpawnContainerErrorMessage);

        _coinPrefab = _coinsSpawnerConfig.CoinPrefab;

        Debug.Assert(_coinPrefab != null, CoinPrefabErrorMessage);

        _player = PlayerInstance.Instance.GetComponent<Player>();

        if (!_player)
            throw new MissingComponentException();

        InitializePool(_coinPrefab.gameObject, _spawnContainer, _player.MaxCropsCount);

        _player.CropAdded += OnCropAdded;
    }

    private void OnDestroy()
    {
        _player.CropAdded -= OnCropAdded;
    }

    private void OnCropAdded(Crop crop)
    {
        crop.Rewarding += OnRewarding;
    }

    private void OnRewarding(Transform containerForSale, Crop crop, int cropPrice)
    {
        crop.Rewarding -= OnRewarding;

        SpawnCoin(containerForSale);
    }

    private void SpawnCoin(Transform containerForSale)
    {
        if (TryGetObjectFromPool(out GameObject cropPrefabObject))
        {
            cropPrefabObject.transform.position = Camera.main.WorldToScreenPoint(containerForSale.position);
            cropPrefabObject.SetActive(true);
        }
    }
}
