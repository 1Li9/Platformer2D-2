using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Player _player;

    [SerializeField] private TextMeshProUGUI _deadText;
    [SerializeField] private TextMeshProUGUI _healthPoints;
    [SerializeField] private Image _panel;

    [SerializeField] private float _opacityUpTime;

    private Coroutine _currentCoroutine;
    private float _maxOpacity = 1f;

    private void Start()
    {
        _deadText.gameObject.SetActive(false);
        _panel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _player.Health.OnHealthPointsChanged += Change;
        _player.Dead += ShowDeadScreen;
    }

    private void OnDisable()
    {
        _player.Health.OnHealthPointsChanged -= Change;
        _player.Dead -= ShowDeadScreen;
    }

    private void Change(float healthPoints) =>
        _healthPoints.text = healthPoints.ToString();

    private void ShowDeadScreen()
    {
        if(_currentCoroutine != null) 
            _timer.StopCoroutine(_currentCoroutine);

        _deadText.gameObject.SetActive(true);
        _panel.gameObject.SetActive(true);

        _currentCoroutine = _timer.DoActionRepeating(() => UpOpacity());
    }

    private void UpOpacity()
    {
        float currentAlpha = _panel.color.a;
        float alpha = Mathf.MoveTowards(currentAlpha, _maxOpacity, Time.deltaTime / _opacityUpTime);

        Color panelColor = _panel.color;
        panelColor.a = alpha;

        _panel.color = panelColor;

        Color textColor = _deadText.color;
        textColor.a = alpha;

        _deadText.color = textColor;

        if (alpha >= _maxOpacity)
            _timer.StopCoroutine(_currentCoroutine);
    }
}
