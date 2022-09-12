using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    private const string CoinConfigErrorMessage = "ErrorConfig is null";

    [Tooltip("—сылка на ScriptableObject: CoinConfig")]
    [SerializeField] private CoinConfig _coinConfig;

    private int _cropPrice;

    public event UnityAction<Transform> CoinSpawned;
    public event UnityAction<int, Coin> Taking;

    public CoinConfig CoinConfig => _coinConfig;

    private void Awake()
    {
        Debug.Assert(_coinConfig != null, CoinConfigErrorMessage);
    }

    public void Init(Transform destinationPoint, int cropPrice)
    {
        _cropPrice = cropPrice;
        CoinSpawned?.Invoke(destinationPoint);
    }

    public void Take()
    {
        Taking?.Invoke(_cropPrice, this);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
