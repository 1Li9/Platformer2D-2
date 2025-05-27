using System;
using System.Collections;
using UnityEngine;

public class ValueAnimator
{
    private Coroutine _coroutine;
    private int _stepsCount;
    private float _timeStep;
    private MonoBehaviour _context;

    public ValueAnimator(MonoBehaviour context)
    {
        _context = context;
        _stepsCount = 120;
        _timeStep = 1f;
    }

    public void Animate(Action<float> action, float startValue, float finalValue, float animationTime)
    {
        if (_coroutine != null)
        {
            _context.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = _context.StartCoroutine(AnimateCoroutine(action, startValue, finalValue, animationTime));
    }

    private IEnumerator AnimateCoroutine(Action<float> action, float startValue, float finalValue, float animationTime)
    {
        animationTime-= _timeStep;
        animationTime = Mathf.Clamp(animationTime, 0f, _stepsCount);
        float animationStep = _timeStep / _stepsCount;
        float progress = 0f;

        WaitForSecondsRealtime wait = new(animationTime / _stepsCount);

        while (progress < 1f)
        {
            startValue = Mathf.MoveTowards(startValue, finalValue, animationStep);
            progress += animationStep;
            action?.Invoke(startValue);

            yield return wait;
        }

        action?.Invoke(finalValue);
    }
}