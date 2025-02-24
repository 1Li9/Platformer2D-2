using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Coin[] _coins;
    [SerializeField] private float _timePeriod;

    private void Start()
    {
        if (_coins.Length == 0)
            throw new System.ArgumentOutOfRangeException(nameof(_coins) + " length is zero");

        foreach (Coin coin in _coins)
            coin.View.gameObject.SetActive(false);

        _timer.DoActionRepeating(() => Spawn(), _timePeriod);
    }

    private void OnEnable()
    {
        foreach (Coin coin in _coins)
            coin.WasInteracted += GetCoinAmount;
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
            coin.WasInteracted -= GetCoinAmount;
    }

    private int GetCoinAmount(Coin coin)
    {
        if (coin.CanSpawn)
            return 0;

        coin.View.gameObject.SetActive(false);
        coin.CanSpawn = true;

        return 1;
    }

    private void Spawn()
    {
        Coin currentCoin = GetRandomCoin();

        currentCoin.View.gameObject.SetActive(true);
        currentCoin.CanSpawn = false;
    }

    private Coin GetRandomCoin()
    {
        int coinIndex = Random.Range(0, _coins.Length);

        return _coins[coinIndex];
    }
}
