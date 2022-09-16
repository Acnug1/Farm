using TMPro;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(200)]

public class CoinsView : MonoBehaviour
{
    private const string CoinsCounterErrorMessage = "CoinsCounter is null";

    [Tooltip("—сылка на счетчик монеток")]
    [SerializeField] private TMP_Text _coinsCounter;
    [SerializeField] private UnityEvent _onMoneyCountChanged;

    private PlayerWallet _playerWallet;

    private void Awake()
    {
        Debug.Assert(_coinsCounter != null, CoinsCounterErrorMessage);
    }

    private void Start()
    {
        _playerWallet = PlayerInstance.Instance.GetComponent<PlayerWallet>();

        if (!_playerWallet)
            throw new MissingComponentException();

        _coinsCounter.text = _playerWallet.Money.ToString();

        _playerWallet.MoneyCountChanged += OnMoneyCountChanged;
    }

    private void OnDestroy()
    {
        _playerWallet.MoneyCountChanged -= OnMoneyCountChanged;
    }

    private void OnMoneyCountChanged(int money)
    {
        _coinsCounter.text = money.ToString();
        _onMoneyCountChanged?.Invoke();
    }
}
