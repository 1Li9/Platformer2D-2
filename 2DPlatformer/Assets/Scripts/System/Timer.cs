using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Coroutine DoActionRepeating(Action action, float timePeriod) =>
        StartCoroutine(DoActionRepeatingCoroutine(action, timePeriod));

    public Coroutine DoActionRepeating(Action action) =>
        StartCoroutine(DoActionRepeatingCoroutine(action));

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
}
