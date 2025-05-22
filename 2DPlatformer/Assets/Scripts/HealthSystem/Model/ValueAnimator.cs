using System;
using System.Collections;
using UnityEngine;

public class ValueAnimator : MonoBehaviour
{
    [Range(0f, .02f)]
    [SerializeField] private float _animationStep;

    private Coroutine _coroutine;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    public void Animate(Action<float> action, float startValue, float finalValue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = StartCoroutine(AnimateCoroutine(action, startValue, finalValue));
    }

    private IEnumerator AnimateCoroutine(Action<float> action, float startValue, float finalValue)
    {
        float progress = 0f;

        while (progress < 1f)
        {
            startValue = Mathf.MoveTowards(startValue, finalValue, _animationStep);
            progress += _animationStep;
            action?.Invoke(startValue);

            yield return null;
        }

        action?.Invoke(finalValue);
    }
}