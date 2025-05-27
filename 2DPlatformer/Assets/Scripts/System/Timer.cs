using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Coroutine DoActionRepeating(Action action, float timePeriod) =>
        StartCoroutine(DoActionRepeatingCoroutine(action, timePeriod));

    public Coroutine DoActionRepeating(Action action) =>
        StartCoroutine(DoActionRepeatingCoroutine(action));

    public Coroutine DoActionDelayed(Action action, float delayTime) =>
        StartCoroutine(DoActionDelayedCoroutine(action, delayTime));

    public Coroutine DoActionWhileDelayed(Action action, Func<bool> condition, Action callback, float delayTime) =>
        StartCoroutine(DoActionWhileDelayedCoroutine(action, condition, callback, delayTime));

    private IEnumerator DoActionRepeatingCoroutine(Action action, float timePeriod)
    {
        bool isActive = true;
        WaitForSeconds wait = new WaitForSeconds(timePeriod);

        while (isActive)
        {
            action?.Invoke();

            yield return wait;
        }
    }

    private IEnumerator DoActionRepeatingCoroutine(Action action)
    {
        bool isActive = true;

        while (isActive)
        {
            action?.Invoke();

            yield return null;
        }
    }

    private IEnumerator DoActionDelayedCoroutine(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        action?.Invoke();
    }

    private IEnumerator DoActionWhileDelayedCoroutine(Action action, Func<bool> condition, Action callback, float delayTime)
    {
        if (condition == null)
            throw new InvalidOperationException(nameof(condition));
        WaitForSeconds wait = new(delayTime);

        while (condition.Invoke())
        {
            action?.Invoke();

            yield return wait;
        }

        callback?.Invoke();
    }
}