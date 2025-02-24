using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WalletUI : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TextMeshProUGUI _text;

    private void OnEnable() =>
        _wallet.CoinsCountChanged += UpdateText;

    private void OnDisable() =>
        _wallet.CoinsCountChanged -= UpdateText;

    private void Start() =>
        _text = GetComponent<TextMeshProUGUI>();

    private void UpdateText(int coinsCount) =>
        _text.text = coinsCount.ToString();
}