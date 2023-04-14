using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public event Action OnGameRestarted;

    [SerializeField] private Button _restartGameButton;

    private void OnEnable()
    {
        _restartGameButton.onClick.AddListener(() => OnGameRestarted?.Invoke());
    }

    private void OnDisable()
    {
        _restartGameButton.onClick.RemoveAllListeners();
    }
}
