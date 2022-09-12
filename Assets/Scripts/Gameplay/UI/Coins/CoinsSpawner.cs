using UnityEngine;

public class CoinsSpawner : ObjectPool
{
    private const string CoinsSpawnerConfigErrorMessage = "CoinsSpawnerConfig is null";
    private const string SpawnContainerErrorMessage = "SpawnContainer is null";
    private const string CoinDestinationPointErrorMessage = "CoinDestinationPoint is null";
    private const string CoinPrefabErrorMessage = "CoinPrefab is null";

    [Tooltip("Ссылка на ScriptableObject: CoinsSpawnerConfig")]
    [SerializeField] private CoinsSpawnerConfig _coinsSpawnerConfig;
    [Tooltip("Контейнер для спавна монеток")]
    [SerializeField] private Transform _spawnContainer;
    [Tooltip("Пункт назначения, в который переместится монетка после появления")]
    [SerializeField] private Transform _destinationPoint;

    private Player _player;
    private Coin _coinPrefab;
    private Coin _coin;

    private void Start()
    {
        Debug.Assert(_coinsSpawnerConfig != null, CoinsSpawnerConfigErrorMessage);
        Debug.Assert(_spawnContainer != null, SpawnContainerErrorMessage);
        Debug.Assert(_destinationPoint != null, CoinDestinationPointErrorMessage);

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

        SpawnCoin(containerForSale, _destinationPoint, cropPrice);
    }

    private void SpawnCoin(Transform containerForSale, Transform destinationPoint, int cropPrice)
    {
        if (TryGetObjectFromPool(out GameObject coinObject))
        {
            coinObject.transform.position = Camera.main.WorldToScreenPoint(containerForSale.position);
            coinObject.SetActive(true);
            _coin = coinObject.GetComponent<Coin>();
            _coin.Taking += OnTaking;
            _coin.Init(destinationPoint, cropPrice);
        }
    }

    private void OnTaking(int cropPrice, Coin coin)
    {
        coin.Taking -= OnTaking;

        _player.GetReward(cropPrice);
    }
}
