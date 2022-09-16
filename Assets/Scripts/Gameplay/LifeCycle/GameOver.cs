using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(500)]

public class GameOver : MonoBehaviour
{
    private SavedData _savedData;
    private PlayerWallet _playerWallet;
    private VictoryConditions _victoryConditions;
    private int _target;
    private bool _isLevelDone = false;

    public event UnityAction Victory;
    public event UnityAction Defeat;

    public int Target => _target;

    private void Start()
    {
        _savedData = SavedData.Instance;
        _playerWallet = PlayerInstance.Instance.GetComponent<PlayerWallet>();
        _victoryConditions = VictoryConditions.Instance;

        SetTarget(_victoryConditions.TargetAmountOfMoney);

        if (!_playerWallet)
            throw new MissingComponentException();

        _playerWallet.MoneyCountChanged += OnMoneyCountChanged;
    }

    private void OnDestroy()
    {
        _playerWallet.MoneyCountChanged -= OnMoneyCountChanged;
    }

    private void SetTarget(int targetAmountOfMoney)
    {
        int moneyAmountAtBeginningOfLevel = _playerWallet.Money;

        _target = moneyAmountAtBeginningOfLevel + targetAmountOfMoney;
    }

    private void OnMoneyCountChanged(int money)
    {
        CheckVictoryConditions(money, _target);
    }

    private void CheckVictoryConditions(int money, int target)
    {
        if (money >= target && !_isLevelDone)
        {
            _isLevelDone = true;
            _savedData.SaveMoneyData(money);
            Victory?.Invoke();
        }
    }
}
