using UnityEngine;

public struct InputInformation
{
    public InputInformation(float axis, KeyCode keyCode)
    {
        Axis = axis;
        KeyCode = keyCode;
    }

    public float Axis { get; }
    public KeyCode KeyCode { get; }
}
