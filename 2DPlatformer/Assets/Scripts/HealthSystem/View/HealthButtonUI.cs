using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HealthButtonUI : MonoBehaviour
{
    [SerializeField] private InteractableObject _interactable;

    private Button _button;
    private Button.ButtonClickedEvent _buttonClickedEvent;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonClickedEvent = new Button.ButtonClickedEvent();
    }

    private void OnEnable() =>
        _buttonClickedEvent.AddListener(_interactable.Interact);

    private void OnDisable() =>
        _buttonClickedEvent.RemoveListener(_interactable.Interact);

    private void Start() =>
        _button.onClick = _buttonClickedEvent;
}