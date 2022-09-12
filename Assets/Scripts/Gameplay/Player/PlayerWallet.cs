using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]

public class PlayerWallet : MonoBehaviour
{
    private Player _player;

    public event UnityAction<int> MoneyCountChanged;

    public int Money { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.RewardTaken += OnRewardTaken;
    }

    private void OnDisable()
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
