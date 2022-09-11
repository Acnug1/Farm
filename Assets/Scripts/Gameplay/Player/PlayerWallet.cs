using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int _money;

    public void AddMoney(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException();

        _money += amount;
    }
}
