using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCounter;

    private PlayerWallet _playerWallet;

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
    }
}
