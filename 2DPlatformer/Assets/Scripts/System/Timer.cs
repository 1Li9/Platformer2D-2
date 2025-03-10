using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Coroutine DoActionRepeating(Action action, float timePeriod) =>
        StartCoroutine(DoActionRepeatingCoroutine(action, timePeriod));

    public Coroutine DoActionRepeating(Action action) =>
        StartCoroutine(DoActionRepeatingCoroutine(action));

    public Coroutine DoActionDelayed(Action action, float delayTime) =>
        StartCoroutine(DoActionDelayedCoroutine(action, delayTime));

    private IEnumerator<WaitForSeconds> DoActionRepeatingCoroutine(Action action, float timePeriod)
    {
        bool isActive = true;
        WaitForSeconds wait = new WaitForSeconds(timePeriod);

        while (isActive)
        {
            action?.Invoke();

            yield return wait;
        }
    }

    private IEnumerator<WaitForSeconds> DoActionRepeatingCoroutine(Action action)
    {
        bool isActive = true;

        while (isActive)
        {
            action?.Invoke();

            yield return null;
        }
    }

    private IEnumerator<WaitForSeconds> DoActionDelayedCoroutine(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        action?.Invoke();

        yield break;
    }
}
