using System;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(100)]
[RequireComponent(typeof(Player))]

public class PlayerWallet : MonoBehaviour
{
    private Player _player;

    public event UnityAction<int> MoneyCountChanged;

    public int Money { get; private set; }

    private void Start()
    {
        _player = GetComponent<Player>();

        Money = SavedData.Instance.LoadMoneyData();

        _player.RewardTaken += OnRewardTaken;
    }

    private void OnDestroy()
    {
        _player.RewardTaken -= OnRewardTaken;
    }

    private void OnRewardTaken(int cropPrice)
    {
        AddMoney(cropPrice);
    }

    private void AddMoney(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException();

        Money += amount;
        MoneyCountChanged?.Invoke(Money);
    }
}
