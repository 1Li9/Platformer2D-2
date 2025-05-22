using System.Collections.Generic;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    [SerializeField] private List<UI> _ui;

    private void Awake()
    {
        foreach (UI ui in _ui)
            ui.gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach (UI ui in _ui)
            ui.gameObject.SetActive(true);
    }
}