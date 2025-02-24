using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinView _view;

    public event Func<Coin, int> WasInteracted;

    public CoinView View { get => _view; set => _view = value; }
    public bool CanSpawn { get; set; } = true;

    public int GetCoinAmount() =>
        WasInteracted.Invoke(this);
}
