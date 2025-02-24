using System;
using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private KeyCode[] _buttons;

    public event Action<InputInformation> OnInputChanged;

    private void LateUpdate() =>
        ProcessInput();

    private void ProcessInput() =>
        OnInputChanged?.Invoke(new InputInformation(Input.GetAxisRaw(Horizontal), GetPressedButton()));

    private KeyCode GetPressedButton()
    {
        foreach (KeyCode button in _buttons)
        {
            if (Input.GetKeyDown(button))
                return button;
        }

        return KeyCode.None;
    }
}
