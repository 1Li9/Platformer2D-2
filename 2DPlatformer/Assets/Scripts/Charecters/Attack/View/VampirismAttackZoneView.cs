using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VampirismAttackZoneView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [Range(0f, 1f)]
    [SerializeField] private float _maxAlpha;

    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _player.VampirismAttacked += () => _renderer.color = GetMaxAlphaColor(_renderer.color);
        _player.VampirismIsReloading += (value) => _renderer.color = GetResetedAlphaColor(_renderer.color);
    }

    private void OnDisable()
    {
        _player.VampirismAttacked -= () => _renderer.color = GetMaxAlphaColor(_renderer.color);
        _player.VampirismIsReloading -= (value) => _renderer.color = GetResetedAlphaColor(_renderer.color);
    }

    private Color GetResetedAlphaColor(Color color) =>
        color * new Color(1, 1, 1, 0);

    private Color GetMaxAlphaColor(Color color) =>
        GetResetedAlphaColor(color) + new Color(0, 0, 0, _maxAlpha);

}
