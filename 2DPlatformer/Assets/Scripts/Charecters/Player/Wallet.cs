using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coinsCount = 0;

    public event Action<int> CoinsCountChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinsCount += coin.Amount;
            CoinsCountChanged?.Invoke(_coinsCount);
        }
    }
}